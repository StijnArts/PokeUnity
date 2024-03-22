using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleQueue
    {
        public BattleEngine Battle;

        public BattleQueue(BattleEngine battle)
        {
            this.Battle = battle;
        }
    }
}
