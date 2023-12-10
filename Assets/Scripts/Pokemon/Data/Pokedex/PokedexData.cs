using Assets.Scripts.Pokemon.Pokedex;
using Assets.Scripts.Pokemon.Species.Pikachu;
using System.Collections.Generic;

namespace Assets.Scripts.Pokemon.Data.Pokedex
{
    public abstract class PokedexData
    {
        public string PokedexName;
        public string PokedexId;
        public Dictionary<PokemonIdentifier, List<PokedexEntry>> PokedexEntries;
        public Dictionary<int, PokemonIdentifier> LocalPokedexNumbers;

        public PokedexData(
            string pokedexId,
            string pokedexName,
            Dictionary<PokemonIdentifier, List<PokedexEntry>> pokedexEntries,
            Dictionary<int, PokemonIdentifier> localPokedexNumbers)
        {
            PokedexId = pokedexId;
            PokedexName = pokedexName;
            PokedexEntries = pokedexEntries;
            LocalPokedexNumbers = localPokedexNumbers;
        }
    }

    public class GenerationOnePokedexData : PokedexData
    {
        public GenerationOnePokedexData() : base(
                "generation_one",
                "Generation 1",
                new Dictionary<PokemonIdentifier, List<PokedexEntry>>()
                {
                    {
                        Pikachu.Identifier,
                        new List<PokedexEntry>()
                        {
                            new PokedexEntry()
                            {
                                Version = "red",
                                Text = "When several of these Pokémon gather, their electricity could build and cause lightning storms."
                            },
                            new PokedexEntry()
                            {
                                Version = "blue",
                                Text = "When several of these Pokémon gather, their electricity could build and cause lightning storms."
                            },
                            new PokedexEntry()
                            {
                                Version = "yellow",
                                Text = "It keeps its tail raised to monitor its surroundings. If you yank its tail, it will try to bite you."
                            },
                            new PokedexEntry()
                            {
                                Version = "statium",
                                Text = "Lives in forests away from people. It stores electricity in its cheeks for zapping an enemy if it is attacked."
                            },
                        }
                    }
                },
                new Dictionary<int, PokemonIdentifier>()
                {
                    {25, Pikachu.Identifier}
                }
            )
        {
        }
    }
}
