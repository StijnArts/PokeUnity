using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Controllers
{
    public class PlayerBattleController : BattleController
    {
        public PlayerBattleController(List<PokemonIndividualData> participatingPokemon) : base(participatingPokemon)
        {
        }
    }
}
