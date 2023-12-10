using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public class MoveSet
    {
        public PokemonIdentifier PokemonIdentifier;
        public Dictionary<int, string> LevelUpMoves;
        public List<string> TmMoves;
        public List<string> EggMoves;
        public List<string> TutorMoves;
        public List<string> EventMoves;
        public bool OverridesAllOtherMoveSets = false;
        public MoveSet(
            PokemonIdentifier pokemonIdentifier,
            Dictionary<int, string> levelUpMoves,
            List<string> tmMoves = null,
            List<string> eggMoves = null,
            List<string> tutorMoves = null,
            List<string> eventMoves = null,
            bool overridesAllOtherMoveSets = false)
        {
            PokemonIdentifier = pokemonIdentifier;
            LevelUpMoves = levelUpMoves;
            TmMoves = tmMoves;
            EggMoves = eggMoves;
            TutorMoves = tutorMoves;
            EventMoves = eventMoves;
            OverridesAllOtherMoveSets = overridesAllOtherMoveSets;
        }
    }
}
