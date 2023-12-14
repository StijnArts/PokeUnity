using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Assets.Scripts.Battle
{
    public abstract class BattleController : BattleSide
    {
        public List<PokemonNpc> ActivePokemon;
        public bool FinishedTurn = false;
        public List<PokemonIndividualData> ParticipatingPokemon;
        public int NumberOfMaxActivePokemon = 1;
        public bool IsSelectingParticipatingPokemon = false;
        public bool HasActivePokemon = false;
        public PokemonIndividualData FaintedLastTurn;
        public PokemonIndividualData FaintedThisTurn;
        public int TotalFainted => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp <= 0).Count();
        public int PokemonLeft => ParticipatingPokemon.Where(pokemon => pokemon.CurrentHp >= 1).Count();
        public abstract void SelectMove(PokemonIndividualData pokemonToMove, PokemonMove move, List<Target> targets);

        public abstract bool SelectParticipatingPokemon(int numberAllowed = 0);

        //TODO make it create pokemonNpc's for the amount of necessary active pokemon in the battle, and set the individual data of those npcs;
        public abstract bool CreateActivePokemon(int minimumAmountOfActivePokemon);
    }
}
