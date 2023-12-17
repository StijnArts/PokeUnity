using Assets.Scripts.Battle.Events;
using Assets.Scripts.Pokemon.Data.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Battle
{
    public class PokemonBattleData
    {
        public static double[] BoostTable = new double[]{ 1, 1.5, 2, 2.5, 3, 3.5, 4 };
        [HideInInspector]
        public bool HasMovedThisTurn = false;
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

        public PokemonBattleData(PokemonIndividualData pokemon, BattleController battleController, Battle battle)
        {
            BattleController = battleController;
            Battle = battle;
            Species = pokemon.BaseSpecies;
            MoveSlots = (MoveSlot[])pokemon.Moves.Clone();
            this.Types = pokemon.BaseSpecies.Types;
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

            if(unmodified && Battle.Field.PseudoWeathers.ContainsKey("wonderroom"))
            {
                if(statType == PokemonStats.StatTypes.Defence)
                {
                    statType = PokemonStats.StatTypes.SpecialDefence;
                } else if (statType == PokemonStats.StatTypes.SpecialDefence)
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
            if(sourceEffect == null && Battle.Event != null) 
            { 
                sourceEffect = Battle.Effect;
            }
            
            foreach(var moveSlot in MoveSlots)
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
            if(AttackedBy.Count == 0) return null;
            return AttackedBy.Last();
        }
    }
}
