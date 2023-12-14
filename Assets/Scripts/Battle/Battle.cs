using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using Assets.Scripts.Battle.Events.Sources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class Battle : MonoBehaviour, Target, EffectHolder
    {
        public BattleSettings _battleSettings;
        public bool BattleIsReady;
        public bool PreparingBattle;
        public List<BattleController> Participants;
        public Dictionary<BattleController, List<PokemonNpc>> ActivePokemon;
        public BattleField Field;
        private int _eventDepth = 0;
        public BattleManager BattleManager => ServiceLocator.Instance.BattleManager;
        public bool StartOfPokemonTurn = true;
        private void Start()
        {
            enabled = false;
        }

        public void PrepareBattle(List<BattleController> participants)
        {
            Participants = participants;
            enabled = true;
        }

        private void Update()
        {

            if (!BattleIsReady)
            {
                StartCoroutine(PrepareBattle());
            }
            else
            {
                if (ParticipantsAreReady())
                {
                    if (AllPokemonHaveMoved())
                    {
                        ResetParticipantTurnState();
                    }
                    else
                    {
                        PlayTurn();
                    }
                }
                else
                {

                }

            }
        }

        IEnumerator PrepareBattle()
        {
            yield return new WaitUntil(BattleIsPrepared);
            BattleIsReady = true;
        }

        public void PlayTurn()
        {
            var executingPokemon = GetActivePokemonIndividualData().Where(pokemon => !pokemon.BattleData.HasMovedThisTurn).First();
            if (StartOfPokemonTurn)
            {
                DecideMoveOrder();
                StartOfPokemonTurn = false;
            }
            executingPokemon.BattleData.PlayTurn();
        }

        private void DecideMoveOrder()
        {
            GetActivePokemonWithBattleControllers().ForEach(activePokemon =>
                activePokemon.Item1.BattleData.CalculateTurnSpeed(activePokemon.Item1.Stats.speed, this, activePokemon.Item2));
            GetActivePokemonIndividualData().Sort((PokemonIndividualData a, PokemonIndividualData b) =>
            {
                if (a.BattleData.Priority > b.BattleData.Priority)
                {
                    return 1;
                }
                else if (a.BattleData.TurnSpeed > b.BattleData.TurnSpeed)
                {
                    return 1;
                }
                else if (a.BattleData.TurnSpeed == b.BattleData.TurnSpeed)
                {
                    var values = new int[] { -1, 1 };
                    var random = new Unity.Mathematics.Random();
                    return values[random.NextInt(0, 1)];
                }

                return -1;
            });
        }

        private bool AllPokemonHaveMoved()
        {
            return GetActivePokemonIndividualData().All(pokemon => pokemon.BattleData.HasMovedThisTurn == true);
        }

        public List<PokemonIndividualData> GetActivePokemonIndividualData()
        {
            return ActivePokemon.SelectMany(value => value.Value).Select(pokemonNpc => pokemonNpc.PokemonIndividualData).ToList();
        }

        public List<Tuple<PokemonIndividualData, BattleController>> GetActivePokemonWithBattleControllers()
        {
            var activePokemonWithControllers = new List<Tuple<PokemonIndividualData, BattleController>>();
            foreach (var activePokemon in ActivePokemon)
            {
                var controller = activePokemon.Key;
                foreach (var pokemon in activePokemon.Value)
                {
                    activePokemonWithControllers.Add(new Tuple<PokemonIndividualData, BattleController>(pokemon.PokemonIndividualData, controller));
                }
            }
            return activePokemonWithControllers;
        }

        private bool ParticipantsAreReady()
        {
            return Participants.All(participant => participant.FinishedTurn);
        }

        private void ResetParticipantTurnState()
        {
            foreach (var participant in Participants)
            {
                participant.FinishedTurn = false;
            }
        }

        private bool BattleIsPrepared()
        {
            var participantsHaveSelectedPokemon = !Participants
                .Where(participant => participant.ParticipatingPokemon == null && !participant.IsSelectingParticipatingPokemon)
                .Select(participant => participant.SelectParticipatingPokemon())
                .Any();
            var participantsHaveActivePokemon = false;
            if (participantsHaveSelectedPokemon && !participantsHaveActivePokemon)
            {
                participantsHaveActivePokemon = !Participants
                .Where(participant => participant.ActivePokemon == null && !participant.HasActivePokemon)
                .Select(participant => participant.CreateActivePokemon(_battleSettings.MinimumAmountOfActivePokemon))
                .Any();
            }


            return participantsHaveSelectedPokemon && participantsHaveActivePokemon;
        }
        //TODO find way to cancel effect with chat message and without;

        //TODO get event modifier and modify it, then save the truncated value to the executing event
        //https://github.com/smogon/pokemon-showdown/blob/d88ccc2107ec890515edd3db8aa95edba615e5e4/SIM_EVENTS.md#the-chainmodify-pattern
        public void ChainModify(double modifier, double? denominator)
        {

        }

        public object RunEvent(string eventid, List<Target> target = null, BattleEventSource source = null, Effect sourceEffect = null, object relayVar = null, bool? onEffect = null, bool? fastExit = null)
        {
            if (_eventDepth >= 8)
            {
                // oh fuck
                //TOdo make it log this for both players in a log file if multiplayer
                Debug.Log("STACK LIMIT EXCEEDED");
                Debug.Log("PLEASE REPORT IN BUG THREAD");
                Debug.Log("Event: " + eventid);
                //Debug.Log("Parent event: " + this.event.id);
                throw new StackOverflowException("Event Depth Exceeded 8");
            }
            target = (target == null ? target = new List<Target>() { this } : target);
            PokemonIndividualData effectSource = (source is PokemonIndividualData ? (PokemonIndividualData)source : null);
            var handlers = FindEventHandlers(target, eventid, effectSource);
            if ((bool)onEffect)
            {
                if (sourceEffect == null) throw new ArgumentNullException("onEffect passed without an effect");
                var callback = sourceEffect.GetCallBack($"on{eventid}");
                if (callback != null)
                {
                    if (target.Count > 1) throw new Exception();
                    handlers.Insert(0, ResolvePriority(new BattleEventListenerWithoutPriority(sourceEffect, callback, , null, target)));
                }
            }
            
        }

        public List<BattleEventListener> FindEventHandlers(List<Target> target, string eventName, PokemonIndividualData source = null)
        {
            if (target.Count < 1)
            {
                throw new ArgumentException("no targets were passed");
            }
            var handlers = new List<BattleEventListener>();
            if(target.Count > 1 && target.All(target => target is PokemonIndividualData))
            {
                for (int i = 0; i < target.Count; i++)
                {
                    var pokemon = (PokemonIndividualData)target[i];
                    var curHandlers = FindEventHandlers(new List<Target>() { pokemon }, eventName, source);
                    foreach (var handler in curHandlers)
                    {
                        handler.Target = pokemon;
                        handler.Index = i;
                    }
                    handlers.AddRange(curHandlers);
                }
            }
            // events usually run through EachEvent should never have any handlers besides `on${eventName}` so don't check for them
            var eachEventEvents = new string[] { "BeforeTurn", "Update", "Weather", "WeatherChange", "TerrainChange" };
            var prefixedHandlers = !eachEventEvents.Any(evt => evt.Contains(eventName));
            var activePokemon = GetActivePokemonIndividualData();
            if(target.Count == 1)
            {
                var singleTarget = target[0];
                if (singleTarget is PokemonIndividualData && (activePokemon.Contains(singleTarget) || (source != null && activePokemon.Contains(source))))
                {
                    var pokemon = singleTarget as PokemonIndividualData;
                    handlers = this.FindPokemonEventHandlers(pokemon, $"on{eventName}");
                    if (prefixedHandlers)
                    {
                        foreach (var allyActive in pokemon.GetAllyAndSelf())
                        {
                            handlers.AddRange(this.FindPokemonEventHandlers(allyActive, $"onAlly{eventName}"));
                            handlers.AddRange(this.FindPokemonEventHandlers(allyActive, $"onAny{eventName}"));
                        }
                        foreach (var foeActive in pokemon.GetFoes(ActivePokemon))
                        {
                            handlers.AddRange(this.FindPokemonEventHandlers(foeActive, $"onFoe{eventName}"));
                            handlers.AddRange(this.FindPokemonEventHandlers(foeActive, $"onAny{eventName}"));
                        }
                    }

                    singleTarget = pokemon.BattleData.BattleController;
                }
                if (source != null && prefixedHandlers)
                {
                    handlers.AddRange(this.FindPokemonEventHandlers(source, $"onSource{eventName}"));
                }
                if (singleTarget is BattleSide)
                {
                    var targetSide = (BattleSide)singleTarget;
                    foreach(var side in Participants)
                    {
                        if (side.SlotNumber >= 2 && side.AllySide != null) break;
                        if (side == targetSide || side == targetSide.AllySide)
                        {
                            handlers.AddRange(this.FindSideEventHandlers(side, $"on{eventName}"));
                        } else if (prefixedHandlers)
                        {
                            handlers.AddRange(this.FindSideEventHandlers(side, $"onFoe{eventName}"));
                        }
                        if(prefixedHandlers)
                        {
                            handlers.AddRange(this.FindSideEventHandlers(side, $"onAny{eventName}"));
                        }
                    }
                }
            }
            handlers.AddRange(this.FindFieldEventHandlers(this.Field, $"on{eventName}"));
            handlers.AddRange(this.FindBattleEventHandlers(this, $"on{eventName}"));
            return handlers;
        }

        public List<BattleEventListener> FindPokemonEventHandlers(PokemonIndividualData pokemon, string callbackName, string getKey = null)
        {
            if (getKey != null) getKey = "duration";
            var handlers = new List<BattleEventListener>();
            var status = pokemon.GetStatus();
            var callback = status.Callbacks[callbackName];
            if(callback != null || (getKey != null && pokemon.StatusState[getKey] != null))
            {
                handlers.Add(this.ResolvePriority(new BattleEventListenerWithoutPriority()
                    { Effect = status, Callback = callback, State = pokemon.StatusState, End = () => pokemon.ClearStatus(), ListenerEffectHolder: pokemon}, callbackName));
            }
        }

        public BattleEventListener ResolvePriority(BattleEventListenerWithoutPriority handler, string callBackName)
        {
            var handlerAsListener = handler as BattleEventListener;
            handlerAsListener.Order = new Tuple<int?, bool>(handler.Effect.GetCallBack(callBackName).Order, handler.Effect.GetCallBack(callBackName).Order != null);
            handlerAsListener.Priority = (handler.Effect.GetCallBack(callBackName).Priority == null ? 0 : handler.Effect.GetCallBack(callBackName).Priority);
            handlerAsListener.SubOrder = (handler.Effect.GetCallBack(callBackName).SubOrder == null ? 0 : handler.Effect.GetCallBack(callBackName).SubOrder);
            if(handlerAsListener.ListenerEffectHolder != null && handlerAsListener.ListenerEffectHolder is PokemonIndividualData)
            {
                handlerAsListener.Speed = (handlerAsListener.ListenerEffectHolder as PokemonIndividualData).BattleSpeed;
            }
            return handlerAsListener;
        }
    }
}
