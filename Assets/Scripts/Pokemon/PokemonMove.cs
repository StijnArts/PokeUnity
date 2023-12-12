using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon
{
    public class PokemonMove
    {
        public int RemainingPowerPoints;
        public Move Move;
        public PokemonMove(Move move)
        {
            Move = move;
            RemainingPowerPoints = move.PowerPoints;
        }
    }
}
