using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Registries
{
    public class EvolutionRegistry
    {
        public static Dictionary<string, Evolution> EvolutionDictionary = new Dictionary<string, Evolution>();
        public static void AddEvolution(string id, Evolution pokemonSpecies)
        {
            if (pokemonSpecies != null)
            {
                EvolutionDictionary.Add(id, pokemonSpecies);
            }
        }

        public static Evolution GetEvolution(string evolutionId)
        {
            Evolution value;
            EvolutionDictionary.TryGetValue(evolutionId, out value);
            return value;
        }

        public static void RegisterPokemonSpecies()
        {
            var foundEvolutions = SubTypeReflector<Evolution>.FindSubTypeClasses();
            foreach (var evolution in foundEvolutions)
            {
                if (evolution != null)
                {
                    EvolutionDictionary.Add(evolution.EvolutionId, evolution);
                    var result = PokemonRegistry.GetPokemonSpecies(evolution.ResultPokemonId);
                    if (!string.IsNullOrEmpty(evolution.ResultPokemonId.FormId))
                    {
                        result.Forms[evolution.ResultPokemonId.FormId].PreEvolution = evolution.EvolvingPokemonId;
                    }
                    else
                    {
                        result.PreEvolution = evolution.EvolvingPokemonId;
                    }

                    var evolvingPokemon = PokemonRegistry.GetPokemonSpecies(evolution.ResultPokemonId);
                    if (!string.IsNullOrEmpty(evolution.ResultPokemonId.FormId))
                    {
                        evolvingPokemon.Forms[evolution.ResultPokemonId.FormId].Evolutions.Add(evolution);
                    }
                    else
                    {
                        evolvingPokemon.Evolutions.Add(evolution);
                    }
                }
            }
        }

        internal static PokemonSpecies GetEvolutionsForSpecies(PokemonIdentifier identifier)
        {
            PokemonSpecies species;
            PokemonRegistry.TryGetPokemon(identifier.SpeciesId, out species);
            return species;
        }
    }
}
