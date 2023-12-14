using Assets.Scripts.Battle.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Battle.Events
{
    public class BattleEventListenerWithoutPriority
    {
        public Effect Effect;
        public PokemonIndividualData Target;
        public int? Index;
        public BattleCallback<object> Callback;
        public EffectState State;
        public Func<object> End;
        public EffectHolder ListenerEffectHolder;
        public object[] EndCallArgs;
    }
}
