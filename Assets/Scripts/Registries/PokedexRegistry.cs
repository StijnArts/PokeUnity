using Assets.Scripts.Pokemon.Data.Pokedex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Registries
{
    //register national dex number if not set
    public class PokedexRegistry
    {
        public static Dictionary<int, PokemonSpecies> NationalPokedexDictionary = new Dictionary<int, PokemonSpecies>();
        public static Dictionary<string, PokedexData> PokedexDictionary = new Dictionary<string, PokedexData>();
        public static void AddPokedex(string id, PokedexData pokedexData)
        {
            if (pokedexData != null)
            {
                PokedexDictionary.Add(id, pokedexData);
            }
        }

        public static PokedexData GetPokemonSpecies(string pokedexId)
        {
            PokedexData value;
            TryGetPokedex(pokedexId, out value);
            return value;
        }

        public static void RegisterPokedexes()
        {
            var foundDexes = SubTypeReflector<PokedexData>.FindSubTypeClasses();
            foreach (PokedexData pokedexData in foundDexes)
            {
                if (pokedexData != null)
                {
                    PokedexDictionary.Add(pokedexData.PokedexId, pokedexData);
                }
            }
            CompleteNationalPokedexNumbering();
            AssignNationalPokedexNumbers();
        }

        private static void AssignNationalPokedexNumbers()
        {
            foreach(var pair in NationalPokedexDictionary)
            {
                pair.Value.NationalPokedexNumber = pair.Key;
            }
        }

        public static void CompleteNationalPokedexNumbering()
        {
            var sortedPokedexData = PokedexDictionary.Values.OrderBy(data => data.PokedexId);
            foreach(var pokedexData in sortedPokedexData)
            {
                var highestDexNumber = NationalPokedexDictionary.Keys.Max();
                foreach(var localNumberEntry in pokedexData.LocalPokedexNumbers.OrderBy(pair => pair.Key))
                {
                    var species = PokemonRegistry.GetPokemonSpecies(localNumberEntry.Value);
                    if (!NationalPokedexDictionary.ContainsValue(species))
                    {
                        NationalPokedexDictionary.Add(highestDexNumber++, species);
                        break;
                    }
                }
            }
            //if any pokemon identifier is in the national dex registery skip entry and add one to the skip counter. skip counter resets when a pokemon that isnt in the dex has been found.
        }

        internal static bool TryGetPokedex(string pokedexId, out PokedexData pokedexData)
        {
            return PokedexDictionary.TryGetValue(pokedexId, out pokedexData);
        }
    }
}
