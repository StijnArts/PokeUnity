using Assets.Scripts.Battle.Events;
using Assets.Scripts.Pokemon.Data.Moves;
using Assets.Scripts.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class PokemonBattleData
    {
        public static double[] BoostTable = new double[] { 1, 1.5, 2, 2.5, 3, 3.5, 4 };
        [HideInInspector]
        public bool HasMovedThisTurn = false;
        public PokemonIndividualData Pokemon;
        public enum BoostableStats { Hp, Attack, Defence, SpecialAttack, SpecialDefence, Speed, Evasion, Accuracy }
        public Dictionary<PokemonStats.StatTypes, int> Boosts;
        public int Priority = 0;
        public int BattleSpeed;
        public BattleController BattleController;
        public Battle Battle;
        public EffectState StatusState;
        public Dictionary<string, EffectState> Volatiles;
        public EffectState AbilityState;
        public EffectState ItemState;
        public int AbilityOrder = 0;
        public PokemonSpecies Species;
        public EffectState SpeciesState;
        public int? SlotPosition;
        public bool Transformed = false;
        public string MoveThisTurn;
        public bool NewlySwitched = false;
        public bool StatRaisedThisTurn = false;
        public bool StatLoweredThisTurn = false;
        public bool HurtThisTurn = false;
        public MoveSlot[] MoveSlots;
        public bool MaybeDisabled = false;
        public ActiveMove LastMove = null;
        public bool IsTerastallized = false;
        public List<Attacker> AttackedBy = new();
        public PokemonIndividualData Illusion;
        public string[] Types;
        public bool KnownType = true;
        public string Terastallized;
        public string AddedType;
        public string ApparentType;
        public bool MaybeTrapped;
        public bool Trapped;
        public int ActiveTurns;
        public string Item;
        public string SwitchFlag;
        public string Ability;

        public string Details;

        public PokemonBattleData(PokemonIndividualData pokemon, BattleController battleController, Battle battle)
        {
            BattleController = battleController;
            Battle = battle;
            Species = pokemon.BaseSpecies;
            MoveSlots = (MoveSlot[])pokemon.Moves.Clone();
            this.Types = pokemon.BaseSpecies.Types;
            Pokemon = pokemon;
            Item = pokemon.Item;
            this.Ability = pokemon.Ability;
        }
        public void CalculateTurnSpeed(int baseSpeed, Battle battle, BattleController battleController)
        {
            double turnSpeed = baseSpeed;



            //TODO implement event system to modify the stat here


            BattleSpeed = turnSpeed > 10000 ? 10000 : (int)turnSpeed;
        }

        internal void PlayTurn()
        {
            throw new NotImplementedException();
        }

        public double GetStat(PokemonIndividualData pokemon, PokemonStats.StatTypes statType, bool unboosted = true, bool unmodified = true)
        {
            if (statType == PokemonStats.StatTypes.Hp) throw new ArgumentException("Please only read maxHp directly");

            double stat = pokemon.BaseStats.GetStat(statType);

            if (unmodified && Battle.Field.PseudoWeathers.ContainsKey("wonderroom"))
            {
                if (statType == PokemonStats.StatTypes.Defence)
                {
                    statType = PokemonStats.StatTypes.SpecialDefence;
                }
                else if (statType == PokemonStats.StatTypes.SpecialDefence)
                {
                    statType = PokemonStats.StatTypes.Defence;
                }
            }

            if (unboosted)
            {
                var boosts = (Dictionary<PokemonStats.StatTypes, int>)Battle.RunEvent("ModifyBoost", new List<Target>() { pokemon }, null, null, Boosts);
                var boost = Math.Clamp(boosts[statType], -6, 6);
                if (boost >= 0)
                {
                    stat = Math.Floor(stat * BoostTable[boost]);
                }
                else
                {
                    stat = Math.Floor(stat / BoostTable[boost]);
                }
            }

            if (unmodified)
            {
                var statTable = new Dictionary<PokemonStats.StatTypes, string>()
                {
                    {PokemonStats.StatTypes.Attack , "Atk"},
                    {PokemonStats.StatTypes.Defence , "Def"},
                    {PokemonStats.StatTypes.SpecialAttack , "SpA"},
                    {PokemonStats.StatTypes.SpecialDefence , "SpD"},
                    {PokemonStats.StatTypes.Speed , "Spe"}
                };
                stat = (double)Battle.RunEvent($"Modify{statTable[statType]}", new List<Target>() { pokemon }, null, null, stat);
            }
            //TODO add truncate method for the format here
            if (statType == PokemonStats.StatTypes.Speed && stat > 10000) stat = 10000;
            return stat;
        }

        public void UpdateSpeed(PokemonIndividualData pokemon)
        {
            BattleSpeed = GetActionSpeed(pokemon);
        }

        public int GetActionSpeed(PokemonIndividualData pokemon)
        {
            double speed = GetStat(pokemon, PokemonStats.StatTypes.Speed, false, false);
            if (Battle.Field.GetPseudoWeather("trickroom") != null)
            {
                speed = 10000 - speed;
            }
            return (int)Math.Floor(Math.Truncate(speed));
        }

        public void DisableMove(string moveId, bool? isHidden = null, Effect sourceEffect = null)
        {
            if (sourceEffect == null && Battle.Event != null)
            {
                sourceEffect = Battle.Effect;
            }

            foreach (var moveSlot in MoveSlots)
            {
                if (moveSlot == null) continue;
                if (moveSlot.Move.Equals(moveId) && !moveSlot.Disabled)
                {
                    moveSlot.Disabled = (isHidden.HasValue && isHidden.Value) || true;
                    moveSlot.DisabledSource = sourceEffect != null ? sourceEffect.Name : moveSlot.Move;
                }
            }
        }

        public Attacker GetLastAttackedBy()
        {
            if (AttackedBy.Count == 0) return null;
            return AttackedBy.Last();
        }

        public bool IsAdjacent(PokemonIndividualData pokemon2)
        {
            if (Pokemon.Fainted || pokemon2.Fainted) return false;
            if (Battle.ActivePerHalf <= 2) return Pokemon != pokemon2;
            if (BattleController == pokemon2.BattleData.BattleController) return Math.Abs(SlotPosition.Value - pokemon2.BattleData.SlotPosition.Value) == 1;
            return Math.Abs(SlotPosition.Value + pokemon2.BattleData.SlotPosition.Value + 1 - BattleController.ActivePokemon.Where(pokemon => pokemon.PokemonIndividualData != null).ToList().Count) <= 1;
        }

        public SwitchRequestData GetSwitchRequestData(bool? forAlly)
        {
            var entry = new SwitchRequestData()
            {
                Identity = Pokemon.GetName(),
                Details = Details,
                Condition = Pokemon.CurrentHp.Value,
                Active = Battle.GetActivePokemonIndividualData().Contains(Pokemon),
                Stats = Pokemon.Stats,
                Moves = (forAlly == true ? Pokemon.Moves : MoveSlots).Select(moveSlot => moveSlot.Move).Select(move =>
                {
                    if ("hiddenpower".Equals(move))
                    {
                        return move + Pokemon.HiddenPowerType + Pokemon.HiddenPowerPower;
                    }
                    if ("frustration".Equals(move) || "return".Equals(move))
                    {
                        var basePowerCallback = MoveRegistry.GetMove(move).GetCallBack("BasePowerCallback");
                        return move + basePowerCallback.Invoke(this);
                    }
                    return move;
                }).ToList(),
                BaseAbility = Pokemon.Ability,
                Item = Item,
                Pokeball = Pokemon.PokeBall
            };
            entry.Ability = Ability;
            entry.Commanding = Volatiles.ContainsKey("commanding") && !Pokemon.Fainted;
            entry.Reviving = Battle.GetActivePokemonIndividualData().Contains(Pokemon) && BattleController.SlotConditions.ContainsKey(SlotPosition.Value)
                && BattleController.SlotConditions[SlotPosition.Value].TryGetValue("revivalblessing", out _);
            entry.TeraType = Pokemon.TeraType;
            entry.Terastallized = Terastallized;

            return entry;
        }

        public MoveRequestData GetMoveRequestData()
        {
            throw new NotImplementedException();
        }
    }
}
