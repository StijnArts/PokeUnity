using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Events
{
    public class BattleEventListener : BattleEventListenerWithoutPriority, SpeedSortable
    {
        public Tuple<int?, bool> Order;
        public int Priority;
        public int SubOrder;
        public int? Speed;
    }
}
