using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Scripts.Battle
{
    public class Effect : BasicEffect
    {
        public EffectType EffectType;
        //TODO add to Ability | Item | ActiveMove | Species | Condition | Format
        //https://stackoverflow.com/questions/540066/calling-a-function-from-a-string-in-c-sharp get callback method with reflection
        public BattleCallback<object> GetCallBack(string callBackName)
        {
            throw new NotImplementedException();
        }
    }
}
