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
        public Battle Battle;
        public int SlotNumber;

        public BattleController AllySide;
        public List<BattleController> Foes;
        public List<FieldEffect> FieldEffects = new();
        public Dictionary<string, bool> GimmickUsed = new();
        public Dictionary<string, EffectState> SideConditions = new();
        public Dictionary<int, Dictionary<string, EffectState>> SlotConditions = new();

        public bool RemoveSideCondition(string statusAsString = null, Effect statusAsEffect = null)
        {
            Effect status = null;
            if (statusAsEffect != null) status = statusAsEffect;
            else if (statusAsString != null) status = ConditionRegistry.GetConditionById(statusAsString);
            else throw new ArgumentNullException("side status cannot be null");

            if (!SideConditions.Keys.Contains(status.Id)) return false;
            var condition = SideConditions[status.Id];
            Battle.SingleEvent("SideEnd", status, condition, this);
            SideConditions[status.Id].Remove(status.Id);

            return true;
        }
    }
}
