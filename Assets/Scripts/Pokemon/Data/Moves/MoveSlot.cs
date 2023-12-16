using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Battle;

namespace Assets.Scripts.Pokemon.Data.Moves
{
    public class MoveSlot
    {
        public int Id;
        public string Move;
        public int PP;
        public int MaxPP;
        public Target.TargettingType? target;
        public bool Disabled = false;
        public string DisabledSource;
        public bool Used;
        public bool? Virtual;
    }
}
