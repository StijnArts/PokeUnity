using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Events
{
    public class BattleCallback<T>
    {
        public Func<Battle, object[], T> Callback;
        public int? Order;
        public int? Priority;
        public int? SubOrder;
        public BattleCallback(Func<Battle, object[], T> callback, int? order = null, int? priority = null, int? subOrder = null) 
        { 
            Callback = callback;
            Order = order;
            Priority = priority;
            SubOrder = subOrder;
        }
    }
}
