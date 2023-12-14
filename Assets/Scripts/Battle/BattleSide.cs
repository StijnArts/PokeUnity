using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleSide : Target, EffectHolder
    {
        public enum SideId { P1, P2, P3, P4 }
        readonly public Battle battle;
        readonly public SideId id;
        readonly public int SlotNumber;

        public BattleController AllySide;
        public BattleController Foe;
        public List<FieldEffect> FieldEffects;
        public Dictionary<string, bool> GimmickUsed;
        public Dictionary<string, EffectState> SideConditions;
        public Dictionary<string, EffectState>[] SlotConditions;
    }
}
