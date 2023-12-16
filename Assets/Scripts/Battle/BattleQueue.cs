using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleQueue
    {
        public Battle Battle;
        public List<Action> actions;

        public BattleQueue(Battle battle)
        {
            this.Battle = battle;
        }
    }
}
