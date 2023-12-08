using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PokemonRegistry

{
    public static Dictionary<string, PokemonData> PokemonDictionary = new Dictionary<string, PokemonData>();
    public static void AddPokemon(string id, PokemonData pokemonData)
    {
        if (pokemonData != null)
        {
            PokemonDictionary.Add(id, pokemonData);
        }
    }

    public static PokemonData GetPokemon(string id)
    {
        PokemonData value;
        PokemonDictionary.TryGetValue(id, out value);
        return value;
    }

    public static string registryToString()
    {
        string list = "Registered Pokemon: ";
        foreach (string id in PokemonDictionary.Keys)
        {
            list += "\n" + id;
            if (PokemonDictionary[id].forms.Count > 0)
            {
                list += "{ ";
                foreach (string formId in PokemonDictionary[id].forms.Keys)
                {
                    list += "\n" + formId+",";
                }
                list += "\n}";
            }
            list += ",";
        }
        return list;
    }

    internal static void RegisterPokemon()
    {
        var pokemonDataStore = Registry<PokemonData>.FindRegistryChildClasses();
        foreach (var pokemonData in pokemonDataStore)
        {
            if (pokemonData != null)
            {
                AddPokemon(pokemonData.PokemonId, pokemonData);
            }
        }
    }
}
