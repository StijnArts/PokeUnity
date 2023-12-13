using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Effects
{
    public abstract class BattleTerrain : FieldEffect
    {
        public abstract override void ApplyFieldEffect(Battle battle, AppliedOn currentBattleStage);
    }
}
