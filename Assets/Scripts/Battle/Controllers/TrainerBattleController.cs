using Assets.Scripts.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Battle.Controllers
{
    public class TrainerBattleController : BattleController
    {
        public override bool CreateActivePokemon(int minimumAmountOfActivePokemon)
        {
            throw new NotImplementedException();
        }

        public override void SelectMove(PokemonIndividualData pokemonToMove, ActiveMove move, List<Target> targets)
        {
            throw new NotImplementedException();
        }

        public override bool SelectParticipatingPokemon(int numberAllowed = 0)
        {
            throw new NotImplementedException();
        }
    }
}
