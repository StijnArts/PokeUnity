using Assets.Scripts.Battle.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Battle.Events
{
    public class BattleEventListenerWithoutPriority : IndexHaver
    {
        public Effect Effect;
        public PokemonIndividualData Target;
        public int? Index;
        public DynamicInvokable Callback;
        public EffectState State;
        public DynamicInvokable End;
        public EffectHolder ListenerEffectHolder;
        public object[] EndCallArgs;

        public BattleEventListenerWithoutPriority(EffectHolder effectHolder = null)
        {
            ListenerEffectHolder = effectHolder;
        }

        public int? GetIndex()
        {
            return Index;
        }
    }
}
