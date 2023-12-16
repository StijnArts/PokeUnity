using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public class MoveHitData : Dictionary<string, MoveHitEntry>
    {

    }

    public class MoveHitEntry
    {
        public bool Crit = false;
        public double TypeMod;
        public bool ZBrokeProtection;

        public MoveHitEntry(bool crit, double typeMod, bool zBrokeProtection) 
        { 
            this.Crit = crit;
            this.TypeMod = typeMod;
            this.ZBrokeProtection = zBrokeProtection;
        }
    }
}
