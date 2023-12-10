using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pokemon.Data
{
    public abstract class Evolution
    {
        public string EvolutionId;
        public PokemonIdentifier ResultPokemonId;
        public PokemonIdentifier EvolvingPokemonId;
        public List<string> LearnableMoves = new List<string>();

        public Evolution() { }
        public Evolution(string id, PokemonIdentifier resultPokemonId, PokemonIdentifier evolvingPokemonId, List<string> learnableMoves = null) 
        {
            EvolutionId = id;
            ResultPokemonId = resultPokemonId;
            EvolvingPokemonId = evolvingPokemonId;
            if(learnableMoves != null)
            {
                LearnableMoves = learnableMoves;
            }
        }

        public abstract bool CheckEvolutionRequirements(Dictionary<string, object> parameters);
        public abstract string ClassToString();
    }
}
