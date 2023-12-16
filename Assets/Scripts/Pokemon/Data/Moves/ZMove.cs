using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data.Moves
{
    public class ZMove
    {
        public int? BasePower;
        public string Effect;
        public Dictionary<PokemonStats.StatTypes, int> Boosts;
    }
}
