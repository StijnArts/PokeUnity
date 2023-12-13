using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public abstract class FieldEffect
    {
        public enum AppliedOn { Switch, StartOfTurn, EndOfTurn, BeforePokemonMove, AfterPokemonMove }
        public abstract void ApplyFieldEffect(Battle battle, AppliedOn currentBattleStage);
    }
}
