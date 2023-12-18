using Assets.Scripts.Pokemon.Data;
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

    public static PokemonSpecies GetPokemonSpecies(string pokemonSpeciesId)
    {
        PokemonSpecies value;
        TryGetPokemon(pokemonSpeciesId, out value);
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

    public static void RegisterPokemonSpecies()
    {
        var foundSpecies = SubTypeReflector<PokemonSpecies>.FindSubTypeClasses();
        foreach (var pokemonSpecies in foundSpecies)
        {
            if (pokemonSpecies != null)
            {
                PokemonDictionary.Add(pokemonSpecies.Id, pokemonSpecies);
                if(pokemonSpecies.NationalPokedexNumber != 0)
                {
                    PokedexRegistry.NationalPokedexDictionary.Add(pokemonSpecies.NationalPokedexNumber, pokemonSpecies);
                }
            }
        }
    }

    internal static bool TryGetPokemon(string pokemonId, out PokemonSpecies species)
    { 
        return PokemonDictionary.TryGetValue(pokemonId, out species);
    }

    internal static PokemonSpecies GetPokemonSpecies(PokemonIdentifier identifier)
    {
        PokemonSpecies species;
        PokemonRegistry.TryGetPokemon(identifier.SpeciesId, out species);
        return species;
    }

    public static void RegisterPokemonForms()
    {
        var foundForms = SubTypeReflector<PokemonForm>.FindSubTypeClasses();
        foreach (var pokemonForm in foundForms)
        {
            if (pokemonForm != null)
            {
                GetPokemonSpecies(pokemonForm.Id).Forms.Add(pokemonForm.FormId, pokemonForm);
            }
        }
    }
}
