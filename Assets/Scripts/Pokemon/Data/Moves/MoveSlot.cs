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
        public Target.TargettingType target;
        public bool Disabled = false;

        public MoveSlot(string move)
        {
            this.Move = move;
            //TODO fetch move stats from the register
        }

        public MoveSlot(Move move)
        {
            this.Move = move.Id;
            this.PP = move.PP;
            this.MaxPP = move.PP;
            this.target = move.MoveTarget;

            //TODO fetch move stats from the register
        }
    }
}
