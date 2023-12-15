using Assets.Scripts.Battle.Events;
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
        public int TurnSpeed;
        public BattleController BattleController;
        public Battle Battle;
        public EffectState StatusState;
        public Dictionary<string, EffectState> Volatiles;
        public EffectState AbilityState;
        public EffectState ItemState;
        public int BattleSpeed;
        public int AbilityOrder = 0;
        public PokemonSpecies Species;
        public EffectState SpeciesState;
        public int? SlotPosition;
        public bool Transformed = false;


        public PokemonBattleData(BattleController battleController, Battle battle)
        {
            BattleController = battleController;
        }
        public void CalculateTurnSpeed(int baseSpeed, Battle battle, BattleController battleController)
        {
            double turnSpeed = baseSpeed;
            
            

             //TODO implement event system to modify the stat here


            TurnSpeed = turnSpeed > 10000 ? 10000 : (int)turnSpeed;
        }

        internal void PlayTurn()
        {
            throw new NotImplementedException();
        }

        public int GetStat(PokemonIndividualData pokemon, PokemonStats.StatTypes statType, bool unboosted = true, bool unmodified = true)
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
    }
}
