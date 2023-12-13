using Assets.Scripts.Battle.Effects;
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
    public class Battle : MonoBehaviour, Target
    {
        public BattleSettings _battleSettings;
        public bool BattleIsReady;
        public bool PreparingBattle;
        public List<BattleController> Participants;
        public Dictionary<BattleController, List<PokemonNpc>> ActivePokemon;
        public List<PseudoWeather> PseudoWeathers = new List<PseudoWeather>();
        public BattleWeather Weathers;
        public Terrain Terrain;
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

        public void RunEvent(string eventid, List<Target> target = null, BattleEventSource source = null, Effect sourceEffect = null, object relayVar = null, bool? onEffect = null, bool? fastExit = null)
        {
            if (_eventDepth >= 8)
            {
                // oh fuck
                this.add("message", "STACK LIMIT EXCEEDED");
                this.add("message", "PLEASE REPORT IN BUG THREAD");
                this.add("message", "Event: " + eventid);
                //this.add("message", "Parent event: " + this.event.id);
                throw new StackOverflowException("Event Depth Exceeded 8");
            }
            target = (target == null ? target = new List<Target>() { this } : target);
            PokemonIndividualData effectSource = (source is PokemonIndividualData ? (PokemonIndividualData)source : null);
            var handlers = FindEventHandlers(target, eventid, effectSource);
            if ((bool)onEffect)
            {
                if (sourceEffect == null) throw new ArgumentNullException("onEffect passed without an effect");
                var callback = sourceEffect.Handlers["on" + eventid];
                if (callback != null)
                {
                    if (target.Count > 1) throw new Exception();
                    handlers.Insert(0, resolvePriority(new EventCallBackWithoutPriority(sourceEffect, callback, , null, target)));
                }
            }
            
        }
    }
}
