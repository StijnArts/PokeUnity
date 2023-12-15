using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using Assets.Scripts.Battle.Events.Sources;
using Assets.Scripts.Registries;
using Assets.Scripts.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class Battle : MonoBehaviour, Target, EffectHolder
    {
        public BattleSettings _battleSettings;
        public bool BattleIsReady;
        public bool PreparingBattle;
        public List<BattleController> Participants;
        public Dictionary<BattleController, List<PokemonNpc>> ActivePokemonDictionary;
        public BattleField Field;
        public Effect Effect;
        public EffectState EffectState;
        private int _eventDepth = 0;
        public BattleManager BattleManager => ServiceLocator.Instance.BattleManager;
        public BattleEvent Event;
        public int AbilityOrder = 0;
        public PokemonIndividualData ActivePokemon;
        public PokemonIndividualData ActiveTarget;
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

        /*private void DecideMoveOrder()
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
        }*/

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

        public bool SuppressingAbility(PokemonIndividualData target = null)
        {
            return ActivePokemon != null && GetActivePokemonIndividualData().Contains(ActivePokemon) && ActivePokemon != target
                && ActiveMove != null && ActiveMove.IgnoreAbility && !target.HasItem("Ability Shield");
        }

        //TODO find way to cancel effect with chat message and without;

        //TODO get event modifier and modify it, then save the truncated value to the executing event
        //https://github.com/smogon/pokemon-showdown/blob/d88ccc2107ec890515edd3db8aa95edba615e5e4/SIM_EVENTS.md#the-chainmodify-pattern
        public void ChainModify(double modifier, double? denominator)
        {

        }

        public double Modify(double value, List<double> modifiers, double? denominator = null)
        {
            var numerator = modifiers[0];
            if (denominator == null) denominator = 1;
            if(modifiers.Count > 1)
            {
                denominator = modifiers[1];
            }

            var modifier = Math.Truncate(numerator * 4096 / denominator.Value);
            return Math.Truncate((Math.Truncate(value * modifier) + 2048 - 1) / 4096);
        }

        public static Comparer<T> ComparePriority<T>() where T : SpeedSortable
        {
            return Comparer<T>.Create((T a, T b) =>
            {
                if (a.GetOrder() < b.GetOrder()) return 1;
                if (a.GetPriority() > b.GetPriority()) return 1;
                if (a.GetSpeed() > b.GetSpeed()) return 1;
                else if (a.GetSpeed() == b.GetSpeed())
                {
                    var values = new int[] { -1, 1 };
                    var random = new Unity.Mathematics.Random();
                    return values[random.NextInt(0, 1)];
                }
                if (a.GetSubOrder() < b.GetSubOrder()) return 1;
                return -1;
            });
        }

        public static Comparer<T> CompareRedirectOrder<T>() where T : SpeedSortable
        {
            return Comparer<T>.Create((T a, T b) =>
            {
                if (a.GetOrder() > b.GetOrder()) return 1;
                if (a.GetPriority() > b.GetPriority()) return 1;
                if (a.GetSpeed() > b.GetSpeed()) return 1;
                else if (a.GetSpeed() == b.GetSpeed())
                {
                    var values = new int[] { -1, 1 };
                    var random = new Unity.Mathematics.Random();
                    return values[random.NextInt(0, 1)];
                }
                if (a is PokemonIndividualData && b is PokemonIndividualData)
                {
                    var pokemonA = a as PokemonIndividualData;
                    var pokemonB = b as PokemonIndividualData;
                    if (pokemonA.BattleData.AbilityOrder > pokemonB.BattleData.AbilityOrder) return 1;
                }
                return -1;
            });
        }

        public static Comparer<T> CompareLeftToRightOrder<T>() where T : SpeedSortable
        {
            return Comparer<T>.Create((T a, T b) =>
            {
                if (a.GetOrder() < b.GetOrder()) return 1;
                if (a.GetPriority() > b.GetPriority()) return 1;
                if (a is IndexHaver && b is IndexHaver)
                {
                    var indexHaverA = a as IndexHaver;
                    var indexHaverB = b as IndexHaver;
                    if (indexHaverA.GetIndex() > indexHaverB.GetIndex()) return 1;
                }
                return -1;
            });
        }


        public void SpeedSort<T>(List<T> sortables, Comparer<T> comparer = null) where T : SpeedSortable
        {
            comparer ??= ComparePriority<T>();
            sortables.Sort(comparer);
        }

        public void EachEvent(string eventid, Effect effect = null, object relayvar = null)
        {
            var actives = GetActivePokemonIndividualData();
            if (effect == null && Effect != null) effect = Effect;
            SpeedSort(actives, Comparer<PokemonIndividualData>.Create((a, b) =>
            {
                if (a.GetSpeed() > b.GetSpeed()) return 1;
                else if (a.GetSpeed() == b.GetSpeed())
                {
                    var values = new int[] { -1, 1 };
                    var random = new Unity.Mathematics.Random();
                    return values[random.NextInt(0, 1)];
                }
                return -1;
            }));
            foreach (var pokemon in actives)
            {
                RunEvent(eventid, new List<Target>() { pokemon }, null, effect, relayvar);
            }
            if (eventid.Equals("Weather"))
            {
                EachEvent("Update");
            }
        }

        public object SingleEvent(string eventid, Effect effect, EffectState state, Target target,
            BattleEventSource source = null, Effect sourceEffect = null, object relayvar = null, object customCallback = null)
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

            var hasRelayValue = true;
            if (relayvar == null)
            {
                relayvar = true;
                hasRelayValue = false;
            }

            if (target is PokemonIndividualData)
            {
                var pokemonTarget = target as PokemonIndividualData;
                if (effect.EffectType == EffectType.Status && !pokemonTarget.Status.Equals(effect.Id))
                {
                    return relayvar;
                }
                if (!eventid.Equals("Start") && !eventid.Equals("TakeItem") && !eventid.Equals("Primal") && effect.EffectType == EffectType.Item
                    && pokemonTarget.IgnoringItem())
                {
                    Debug.Log(eventid + " handler suppressed by Embargo, Klutz or Magic Room");
                    return relayvar;
                }
                if (!eventid.Equals("End") && effect.EffectType == EffectType.Ability && pokemonTarget.IgnoringAbility())
                {
                    Debug.Log(eventid + " handler suppressed by Gastro Acid or Neutralizing Gas");
                    return relayvar;
                }
            }

            if (effect.EffectType == EffectType.Weather && !eventid.Equals("FieldStart") && !eventid.Equals("FieldResidual")
                && !eventid.Equals("FieldEnd") && Field.SuppressingWeather())
            {
                Debug.Log(eventid + " handler suppressed by Air Lock");
                return relayvar;
            }

            var callback = (customCallback != null ? customCallback : effect.GetCallBack($"on{eventid}"));
            if (callback == null) return relayvar;

            var parentEffect = this.Effect;
            var parentEffectState = this.EffectState;
            var parentEvent = this.Event;

            Effect = effect;
            EffectState = (state == null ? new EffectState() : state);
            this.Event = new BattleEvent() { Id = eventid, Target = new List<Target>() { target }, Source = source, Effect = sourceEffect };
            _eventDepth++;

            var args = new List<object>() { target, source, sourceEffect };
            if (hasRelayValue) args.Insert(0, relayvar);

            object returnVal = null;
            if (callback is DynamicInvokable)
            {
                if (callback is VoidBattleCallback)
                {
                    (callback as VoidBattleCallback).Invoke(this, args.ToArray());
                }
                else
                {
                    returnVal = (callback as DynamicInvokable).Invoke(this, args.ToArray());
                }
            }
            else
            {
                returnVal = callback;
            }

            this._eventDepth--;
            this.Effect = parentEffect;
            this.EffectState = parentEffectState;
            this.Event = parentEvent;

            return returnVal == null ? relayvar : returnVal;
        }

        public object RunEvent(string eventid, List<Target> target = null, BattleEventSource source = null, Effect sourceEffect = null, object relayVar = null,
            bool? onEffect = null, bool? fastExit = null)
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
            if (onEffect.HasValue && onEffect.Value)
            {
                if (sourceEffect == null) throw new ArgumentNullException("onEffect passed without an effect");
                var callback = sourceEffect.GetCallBack($"on{eventid}");
                if (callback != null)
                {
                    if (target.Count > 1) throw new Exception();
                    handlers.Insert(0, ResolvePriority(new BattleEventListenerWithoutPriority((EffectHolder)target[0])
                    {
                        Effect = sourceEffect,
                        Callback = callback,
                        State = new EffectState()
                    }, $"on{eventid}"));
                }
            }

            if (new string[] { "Invulnerability", "TryHit", "DamagingHit", "EntryHazard" }.Contains(eventid))
            {
                handlers.Sort(CompareLeftToRightOrder<BattleEventListener>());
            }
            else if (fastExit.HasValue && fastExit.Value)
            {
                handlers.Sort(CompareRedirectOrder<BattleEventListener>());
            }
            else
            {
                SpeedSort(handlers);
            }

            var hasRelayVar = 1;
            var args = new List<object>() { target, source, sourceEffect };
            if (relayVar == null)
            {
                relayVar = true;
                hasRelayVar = 0;
            }
            else
            {
                args.Insert(0, relayVar);
            }

            var parentEvent = Event;
            Event = new BattleEvent() { Id = eventid, Target = target, Source = source, Effect = sourceEffect, Modifier = 1 };
            _eventDepth++;

            var targetRelayVars = new List<object>();
            if (target.Count > 1)
            {
                if (relayVar.GetType().IsArray)
                {
                    targetRelayVars = new List<object>((IEnumerable<object>)relayVar);
                }
                else
                {
                    target.ForEach(target => targetRelayVars.Add(true));
                }
            }

            foreach (var handler in handlers)
            {
                if (handler.Index.HasValue)
                {
                    if (targetRelayVars[handler.Index.Value] is false && !(targetRelayVars[handler.Index.Value] is 0 && "DamagingHit".Equals(eventid))) continue;
                    if (handler.Target != null)
                    {
                        args[hasRelayVar] = handler.Target;
                        Event.Target = new List<Target>() { handler.Target };
                    }
                    if (hasRelayVar == 1)
                    {
                        args[0] = targetRelayVars[handler.Index.Value];
                    }
                }

                var effect = handler.Effect;
                var effectHolder = handler.ListenerEffectHolder;
                if (effect.EffectType == EffectType.Status && effectHolder is PokemonIndividualData
                    && !(effectHolder as PokemonIndividualData).Status.Equals(effect.Id)) continue;

                if (effect.EffectType == EffectType.Ability && effect is Ability && (effect as Ability).IsBreakable != false &&
                    effectHolder is PokemonIndividualData && SuppressingAbility(effectHolder as PokemonIndividualData))
                {
                    var ability = effect as Ability;
                    if (ability.IsBreakable == true)
                    {
                        Debug.Log(eventid + " handler suppressed by Mold Breaker");
                        continue;
                    }

                    if (effect.Num == null || effect.Num.Value == 0)
                    {
                        var attackingEvents = new List<string>()
                        {
                            "BeforeMove",
                        "BasePower",
                        "Immunity",
                        "RedirectTarget",
                        "Heal",
                        "SetStatus",
                        "CriticalHit",
                        "ModifyAtk", "ModifyDef", "ModifySpA", "ModifySpD", "ModifySpe", "ModifyAccuracy",
                        "ModifyBoost",
                        "ModifyDamage",
                        "ModifySecondaries",
                        "ModifyWeight",
                        "TryAddVolatile",
                        "TryHit",
                        "TryHitSide",
                        "TryMove",
                        "Boost",
                        "DragOut",
                        "Effectiveness",
                        };
                        if (attackingEvents.Contains(eventid))
                        {
                            Debug.Log(eventid + " handler suppressed by Mold Breaker");
                            continue;
                        }
                        else if ("Damage".Equals(eventid) && sourceEffect != null && sourceEffect.EffectType == EffectType.Move)
                        {
                            Debug.Log(eventid + " handler suppressed by Mold Breaker");
                            continue;
                        }
                    }
                }

                if (!"Start".Equals(eventid) && !"SwitchIn".Equals(eventid) && !"TakeItem".Equals(eventid) && effect.EffectType == EffectType.Item
                    && effectHolder is PokemonIndividualData && (effectHolder as PokemonIndividualData).IgnoringItem())
                {
                    if (!"Update".Equals(eventid))
                    {
                        Debug.Log(eventid + " handler suppressed by Embargo, Klutz or Magic Room");
                    }
                    continue;
                }
                else if (!"End".Equals(eventid) && effect.EffectType == EffectType.Ability
                    && effectHolder is PokemonIndividualData && (effectHolder as PokemonIndividualData).IgnoringAbility())
                {
                    if (!"Update".Equals(eventid))
                    {
                        Debug.Log(eventid + " handler suppressed by Gastro Acid or Neutralizing Gas");
                    }
                    continue;
                }
                if ((effect.EffectType == EffectType.Weather || "Weather".Equals(eventid)) && !"Residual".Equals(eventid) && !"End".Equals(eventid) && Field.SuppressingWeather())
                {
                    Debug.Log(eventid + " handler suppressed by Air Lock");
                    continue;
                }

                object returnVal = null;
                if (handler.Callback is DynamicInvokable)
                {
                    var parentEffect = Effect;
                    var parentEffectState = EffectState;
                    Effect = handler.Effect;
                    EffectState = handler.State;
                    if (EffectState.ContainsKey("target"))
                    {
                        EffectState["target"] = effectHolder;
                    } else EffectState.Add("target", effectHolder);

                    if (handler.Callback is VoidBattleCallback)
                    {
                        (handler.Callback as VoidBattleCallback).Invoke(this, args.ToArray());
                    }
                    else
                    {
                        returnVal = (handler.Callback as DynamicInvokable).Invoke(this, args.ToArray());
                    }

                    Effect = parentEffect;
                    EffectState = parentEffectState;
                }
                else
                {
                    returnVal = handler.Callback;
                }

                if(returnVal != null)
                {
                    relayVar = returnVal;
                    if (relayVar is false || fastExit.Value)
                    {
                        if (handler.Index != null)
                        {
                            targetRelayVars[handler.Index.Value] = relayVar;
                            if (targetRelayVars.All(value => value is false)) break;
                        }
                        else break;
                    }
                    if(hasRelayVar == 1)
                    {
                        args[0] = relayVar;
                    }
                }
            }

            _eventDepth--;
            if(relayVar != null && TypeUtils.IsNumber(relayVar) && (relayVar as double?).Value == Math.Abs(Math.Floor((relayVar as double?).Value)))
            {
                relayVar = Modify((relayVar as double?).Value, Event.Modifier);
            }
            Event = parentEvent;

            return target.Count > 1 ? targetRelayVars : relayVar;
        }

        public BattleEventListener ResolvePriority(BattleEventListenerWithoutPriority handler, string callBackName)
        {
            var handlerAsListener = handler as BattleEventListener;
            handlerAsListener.Order = new Tuple<int?, bool>(handler.Effect.GetCallBack(callBackName).Order, handler.Effect.GetCallBack(callBackName).Order != null);
            handlerAsListener.Priority = (handler.Effect.GetCallBack(callBackName).Priority == null ? 0 : handler.Effect.GetCallBack(callBackName).Priority.Value);
            handlerAsListener.SubOrder = (handler.Effect.GetCallBack(callBackName).SubOrder == null ? 0 : handler.Effect.GetCallBack(callBackName).SubOrder.Value);
            if (handlerAsListener.ListenerEffectHolder != null && handlerAsListener.ListenerEffectHolder is PokemonIndividualData)
            {
                handlerAsListener.Speed = (handlerAsListener.ListenerEffectHolder as PokemonIndividualData).BattleData.BattleSpeed;
            }
            return handlerAsListener;
        }

        public List<BattleEventListener> FindEventHandlers(List<Target> target, string eventName, PokemonIndividualData source = null)
        {
            if (target.Count < 1)
            {
                throw new ArgumentException("no targets were passed");
            }
            var handlers = new List<BattleEventListener>();
            if (target.Count > 1 && target.All(target => target is PokemonIndividualData))
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
            // events usually run through EachEvent should never have any handlers besides `on${eventName}` so don"t check for them
            var eachEventEvents = new string[] { "BeforeTurn", "Update", "Weather", "WeatherChange", "TerrainChange" };
            var prefixedHandlers = !eachEventEvents.Any(evt => evt.Contains(eventName));
            var activePokemon = GetActivePokemonIndividualData();
            if (target.Count == 1)
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
                    foreach (var side in Participants)
                    {
                        if (side.SlotNumber >= 2 && side.AllySide != null) break;
                        if (side == targetSide || side == targetSide.AllySide)
                        {
                            handlers.AddRange(this.FindSideEventHandlers(side, $"on{eventName}"));
                        }
                        else if (prefixedHandlers)
                        {
                            handlers.AddRange(this.FindSideEventHandlers(side, $"onFoe{eventName}"));
                        }
                        if (prefixedHandlers)
                        {
                            handlers.AddRange(this.FindSideEventHandlers(side, $"onAny{eventName}"));
                        }
                    }
                }
            }
            handlers.AddRange(this.FindFieldEventHandlers(this.Field, $"on{eventName}"));
            handlers.AddRange(this.FindBattleEventHandlers($"on{eventName}"));
            return handlers;
        }

        public List<BattleEventListener> FindPokemonEventHandlers(PokemonIndividualData pokemon, string callbackName, string getKey = null)
        {
            if (getKey != null) getKey = "duration";
            var handlers = new List<BattleEventListener>();
            var status = pokemon.GetStatus();
            var callback = status.GetCallBack(callbackName);
            if (callback != null || (getKey != null && pokemon.BattleData.StatusState.ContainsKey(getKey) && pokemon.BattleData.StatusState[getKey] != null))
            {
                handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(pokemon)
                { Effect = status, Callback = callback, State = pokemon.BattleData.StatusState, End = new BattleCallback<bool>(() => pokemon.ClearStatus()) }, callbackName));
            }
            foreach (var volatileId in pokemon.BattleData.Volatiles.Keys)
            {
                var volatileState = pokemon.BattleData.Volatiles[volatileId];
                var volatileStatus = ConditionRegistry.GetConditionById(volatileId);
                callback = volatileStatus.GetCallBack(callbackName);
                if (callback != null || (getKey != null && volatileState.ContainsKey(getKey) && volatileState[getKey] != null))
                {
                    var end = new BattleCallback<string, bool>((string volatileId) => pokemon.RemoveVolatile(volatileId));
                    handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(pokemon)
                    //TODO ask how this works without passing a variable to remove volatile
                    { Effect = volatileStatus, Callback = callback, State = volatileState, End = end }, callbackName));
                }
            }
            var ability = pokemon.GetAbility();
            callback = ability.GetCallBack(callbackName);
            if (callback != null || (getKey != null && pokemon.BattleData.AbilityState.ContainsKey(getKey) && pokemon.BattleData.AbilityState[getKey] != null))
            {
                handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(pokemon)
                { Effect = ability, Callback = callback, State = pokemon.BattleData.AbilityState, End = new BattleCallback<string>(() => pokemon.ClearAbility()) }, callbackName));
            }
            var item = pokemon.GetItem();

            callback = item.GetCallBack(callbackName);
            if (callback != null || (getKey != null && pokemon.BattleData.ItemState.ContainsKey(getKey) && pokemon.BattleData.ItemState[getKey] != null))
            {
                handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(pokemon)
                { Effect = item, Callback = callback, State = pokemon.BattleData.ItemState, End = new BattleCallback<bool>(() => pokemon.ClearItem()) }, callbackName));
            }

            var species = pokemon.BattleData.Species;
            callback = species.GetCallBack(callbackName);
            if (callback != null)
            {
                handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(pokemon)
                { Effect = species, Callback = callback, State = pokemon.BattleData.SpeciesState }, callbackName));
            }

            var side = pokemon.BattleData.BattleController;
            foreach (var conditionId in side.SlotConditions[pokemon.BattleData.BattleController.GetSlotNumber(pokemon).Value].Keys)
            {
                var slotConditionState = side.SlotConditions[pokemon.BattleData.BattleController.GetSlotNumber(pokemon).Value][conditionId];
                var slotCondition = ConditionRegistry.GetConditionById(conditionId);
                callback = slotCondition.GetCallBack(callbackName);
                if (callback != null || (getKey != null && slotConditionState.ContainsKey(getKey) && slotConditionState[getKey] != null))
                {
                    handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(pokemon)
                    {
                        Effect = species,
                        Callback = callback,
                        State = pokemon.BattleData.SpeciesState,
                        End = new BattleCallback<int?, PokemonIndividualData, string, Effect, bool>(
                            (int? slotAsInt, PokemonIndividualData slotAsPokemon, string statusAsString, Effect statusAsEffect) =>
                            side.RemoveSlotCondition(slotAsInt, slotAsPokemon, statusAsString, statusAsEffect)),
                        EndCallArgs = new object[] { side, pokemon, slotCondition.Id }
                    }, callbackName));
                }
            }

            return handlers;
        }

        public List<BattleEventListener> FindSideEventHandlers(BattleSide side, string callbackName, string getKey = null, PokemonIndividualData customHolder = null)
        {
            if (getKey != null) getKey = "duration";
            var handlers = new List<BattleEventListener>();
            foreach (var conditionId in side.SideConditions.Keys)
            {
                var sideConditionState = side.SideConditions[conditionId];
                var sideCondition = ConditionRegistry.GetConditionById(conditionId);
                var callback = sideCondition.GetCallBack(callbackName);
                if (callback != null || (getKey != null && sideConditionState.ContainsKey(getKey) && sideConditionState[getKey] != null))
                {
                    handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(customHolder != null ? customHolder : side)
                    {
                        Effect = sideCondition,
                        Callback = callback,
                        State = sideConditionState,
                        End = customHolder != null ? null : new BattleCallback<string, Effect, bool>(
                            (string statusAsString, Effect statusAsEffect) =>
                            side.RemoveSideCondition(statusAsString, statusAsEffect))
                    }, callbackName));
                }
            }
            return handlers;
        }

        public List<BattleEventListener> FindBattleEventHandlers(string callbackName, string getKey = null)
        {
            if (getKey != null) getKey = "duration";
            var handlers = new List<BattleEventListener>();
            DynamicInvokable callback;
            //TODO implement battle formats
            return handlers;
        }

        public List<BattleEventListener> FindFieldEventHandlers(BattleField field, string callbackName, string getKey = null, PokemonIndividualData customHolder = null)
        {
            if (getKey != null) getKey = "duration";
            var handlers = new List<BattleEventListener>();
            DynamicInvokable callback;
            foreach (var pseudoWeatherId in Field.PseudoWeathers.Keys)
            {
                var pseudoWeatherState = Field.PseudoWeathers[pseudoWeatherId];
                var pseudoWeather = ConditionRegistry.GetConditionById(pseudoWeatherId);
                callback = pseudoWeather.GetCallBack(callbackName);
                if (callback != null || (getKey != null && pseudoWeatherState.ContainsKey(getKey) && pseudoWeatherState[getKey] != null))
                {
                    handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(customHolder != null ? customHolder : field)
                    {
                        Effect = pseudoWeather,
                        Callback = callback,
                        State = pseudoWeatherState,
                        End = customHolder != null ? null :
                    new BattleCallback<string, Effect, bool>(
                            (string statusAsString, Effect statusAsEffect) => field.RemovePseudoWeather(statusAsString, statusAsEffect))
                    }, callbackName));
                }
            }

            var weather = Field.GetWeather();
            callback = weather.GetCallBack(callbackName);
            if (callback != null || (getKey != null && Field.WeatherState.ContainsKey(getKey) && Field.WeatherState[getKey] != null))
            {
                handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(customHolder != null ? customHolder : field)
                {
                    Effect = weather,
                    Callback = callback,
                    State = Field.WeatherState,
                    End = customHolder != null ? null :
                    new BattleCallback<bool>(
                            () => field.ClearWeather())
                }, callbackName));
            }

            var terrain = Field.GetTerrain();
            callback = terrain.GetCallBack(callbackName);
            if (callback != null || (getKey != null && Field.TerrainState.ContainsKey(getKey) && Field.TerrainState[getKey] != null))
            {
                handlers.Add(ResolvePriority(new BattleEventListenerWithoutPriority(customHolder != null ? customHolder : field)
                {
                    Effect = terrain,
                    Callback = callback,
                    State = Field.TerrainState,
                    End = customHolder != null ? null :
                    new BattleCallback<bool>(
                            () => field.ClearTerrain())
                }, callbackName));
            }

            return handlers;
        }
    }
}
