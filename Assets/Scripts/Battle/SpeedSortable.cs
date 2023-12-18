using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public interface SpeedSortable
    {

        public int? GetSpeed() => 0;
        public int? GetPriority() => 0;
        public int? GetOrder() => 0;
        public int? GetSubOrder() => 0;
    }
}
