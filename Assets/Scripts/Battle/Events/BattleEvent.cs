using Assets.Scripts.Battle.Events.Sources;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Events
{
    public class BattleEvent
    {
        public string Id;
        public List<Target> Target;
        public BattleEventSource Source;
        public Effect Effect;
        public double? Modifier;
    }
}
