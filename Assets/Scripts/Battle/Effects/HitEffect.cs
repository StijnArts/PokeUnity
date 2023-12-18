using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Effects
{
    public interface HitEffect
    {
        public OnHitEvent OnHit() => null;
        public Dictionary<PokemonStats.StatTypes, int> GetBoosts() => null;
        public string GetStatus() => null;
        public string GetVolatileStatus() => null;
        public string GetSideCondition() => null;
        public string GetSlotCondition() => null;
        public string GetPseudoWeather() => null;
        public string GetTerrain() => null;
        public string GetWeather() => null;
    }
}
