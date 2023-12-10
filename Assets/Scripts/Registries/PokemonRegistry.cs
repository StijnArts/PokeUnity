using Assets.Scripts.Registries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PokemonRegistry

{
    public static Dictionary<string, PokemonSpecies> PokemonDictionary = new Dictionary<string, PokemonSpecies>();
    public static void AddPokemon(string id, PokemonSpecies pokemonSpecies)
    {
        if (pokemonSpecies != null)
        {
            PokemonDictionary.Add(id, pokemonSpecies);
        }
    }

    public static PokemonSpecies GetPokemonSpecies(string id)
    {
        PokemonSpecies value;
        PokemonDictionary.TryGetValue(id, out value);
        return value;
    }

    public static string registryToString()
    {
        string list = "Registered Pokemon: ";
        foreach (string id in PokemonDictionary.Keys)
        {
            list += "\n" + id;
            if (PokemonDictionary[id].Forms.Count > 0)
            {
                list += "{ ";
                foreach (string formId in PokemonDictionary[id].Forms.Keys)
                {
                    list += "\n" + formId+",";
                }
                list += "\n}";
            }
            list += ",";
        }
        return list;
    }

    public static Dictionary<string, PokemonSpecies> RegisterPokemonSpecies()
    {
        var registry = new Dictionary<string, PokemonSpecies>();
        var foundSpecies = SubTypeReflector<PokemonSpecies>.FindSubTypeClasses();
        foreach (var pokemonSpecies in foundSpecies)
        {
            if (pokemonSpecies != null)
            {
                registry.Add(pokemonSpecies.PokemonId, pokemonSpecies);
                if(pokemonSpecies.NationalPokedexNumber != 0)
                {
                    NationalPokedex.NationalPokedexDictionary.Add(pokemonSpecies.NationalPokedexNumber, pokemonSpecies);
                }
            }
        }

        return registry;
    }
}
