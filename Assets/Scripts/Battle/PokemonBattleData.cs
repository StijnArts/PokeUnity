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
        public PokemonBattleData(BattleController battleController, Battle battle)
        {
            BattleController = battleController;
        }
        public void CalculateTurnSpeed(int baseSpeed, Battle battle, BattleController battleController)
        {
            double turnSpeed = baseSpeed;
            int boost = Math.Clamp(Boosts[PokemonStats.StatTypes.Speed],-6, 6);
            if (boost >= 0)
            {
                turnSpeed = Math.Floor(turnSpeed * BoostTable[boost]);
            } else
            {
                turnSpeed = Math.Floor(turnSpeed / BoostTable[boost]);
            }

             //TODO implement event system to modify the stat here


            TurnSpeed = turnSpeed > 10000 ? 10000 : (int)turnSpeed;
        }

        internal void PlayTurn()
        {
            throw new NotImplementedException();
        }

        public bool PerformAction;

        public int GetStat(PokemonStats.StatTypes statType, bool unboosted = true, bool unmodified = true)
        {
            if (stat == PokemonStats.StatTypes.Hp) throw new ArgumentException("Please only read maxHp directly");

            var stat
        }
    }
}
