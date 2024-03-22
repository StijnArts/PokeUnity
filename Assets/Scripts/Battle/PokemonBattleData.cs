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
        public int AbilityOrder = 0;
        public PokemonSpecies Species;
        public int? SlotPosition;
        public bool Transformed = false;
        public bool NewlySwitched = false;
        public bool StatRaisedThisTurn = false;
        public bool StatLoweredThisTurn = false;
        public bool HurtThisTurn = false;

        public string Details;

        public PokemonBattleData(PokemonIndividualData pokemon, BattleController battleController, BattleEngine battle)
        {
            Species = pokemon.BaseSpecies;
            Pokemon = pokemon;
        }
    }
}
