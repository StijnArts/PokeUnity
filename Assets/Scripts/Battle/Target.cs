using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle
{
    public interface Target
    {
        public enum TargettingType
        { AdjacentAlly, AdjacentAllyOrSelf, AdjacentFoe, Aall, AllAdjacent, AllAdjacentFoes, Allies, AllySide, AllyTeam, Any, FoeSide, 
            Normal, RandomNormal, Scripted, Self }
    }
}
