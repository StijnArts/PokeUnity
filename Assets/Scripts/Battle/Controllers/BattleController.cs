using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Battle
{
    public abstract class BattleController : BattleSide
    {
        public string Name;
        public string Id;
        public List<PokemonNpc> ActivePokemon;
        public bool FinishedTurn = false;
        public List<PokemonIndividualData> ParticipatingPokemon;
        public int NumberOfMaxActivePokemon = 1;
        public bool IsSelectingParticipatingPokemon = false;
        public bool HasActivePokemon = false;
        public bool Ready = false;
        public PokemonIndividualData FaintedLastTurn;
        public PokemonIndividualData FaintedThisTurn;
        public int TotalFainted => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp <= 0).Count();
        public int PokemonLeft => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp >= 1).Count();

        public BattleController(List<PokemonIndividualData> participatingPokemon)
        {
            ParticipatingPokemon = participatingPokemon;
        }
    }
}
