using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Events.Sources
{
    public class BooleanEventSource : BattleEventSource
    {
        public bool Source;

        public BooleanEventSource(bool source)
        {
            Source = source;
        }
    }
}
