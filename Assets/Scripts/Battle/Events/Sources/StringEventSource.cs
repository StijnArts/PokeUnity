using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Events.Sources
{
    public class StringEventSource : BattleEventSource
    {
        public string Source;

        public StringEventSource(string source) 
        {
            Source = source;
        }
    }
}
