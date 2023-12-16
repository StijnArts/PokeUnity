using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using Assets.Scripts.Registries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class BattleSide : Target, EffectHolder
    {
        readonly public Battle battle;
        readonly public int Id;
        readonly public int SlotNumber;

        public BattleController AllySide;
        public BattleController Foe;
        public List<FieldEffect> FieldEffects;
        public Dictionary<string, bool> GimmickUsed;
        public Dictionary<string, EffectState> SideConditions;
        public Dictionary<int, Dictionary<string, EffectState>> SlotConditions;

        public bool RemoveSideCondition(string statusAsString = null, Effect statusAsEffect = null)
        {
            Effect status = null;
            if (statusAsEffect != null) status = statusAsEffect;
            else if (statusAsString != null) status = ConditionRegistry.GetConditionById(statusAsString);
            else throw new ArgumentNullException("side status cannot be null");

            if (!SideConditions.Keys.Contains(status.Id)) return false;
            var condition = SideConditions[status.Id];
            battle.SingleEvent("SideEnd", status, condition, this);
            SideConditions[status.Id].Remove(status.Id);

            return true;
        }
    }
}
