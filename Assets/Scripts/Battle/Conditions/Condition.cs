using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Conditions
{
    public class Condition : Effect
    {
        internal BattleCallback<object> GetCallBack(string callbackName)
        {
            throw new NotImplementedException();
        }
    }
}
