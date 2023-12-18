using Assets.Scripts.Pokemon.PokemonTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PokemonTypeRegistry
{
    public static Dictionary<string, PokemonType> PokemonTypes = new Dictionary<string, PokemonType>();
    public static void registerPokemonType(string id, PokemonType pokemonType)
    {
        if (pokemonType != null)
        {
            PokemonTypes.Add(id, pokemonType);
        }
    }

    public static PokemonType GetType(string id)
    {
        PokemonType value;
        PokemonTypes.TryGetValue(id, out value);
        return value;
    }

    public static string registryToString()
    {
        string list = "Registered Types: ";
        foreach (string id in PokemonTypes.Keys)
        {
            list += "\n" + id;
            if (PokemonTypes[id].Interactions.Count > 0)
            {
                list += "\n\tsuper effective on: { ";
                foreach (string weakness in PokemonTypes[id].Interactions.Keys.Where(
                    key => PokemonTypes[id].Interactions[key] == PokemonType.TypeInteraction.STRONG_AGAINST)
                    )
                {
                    list += "\n\t\t" + weakness + ",";
                }
                list += "\n\t},";
            
            
                list += "\n\tresisted by: { ";
                foreach (string resistance in PokemonTypes[id].Interactions.Keys.Where(
                    key => PokemonTypes[id].Interactions[key] == PokemonType.TypeInteraction.RESISTED_BY)
                    )
                {
                    list += "\n\t\t" + resistance + ",";
                }
                list += "\n\t},";
            
                list += "\n\ttypes with immunity: { ";
                foreach (string immunity in PokemonTypes[id].Interactions.Keys.Where(
                    key => PokemonTypes[id].Interactions[key] == PokemonType.TypeInteraction.HAS_IMMUNITY)
                    )
                { 
                    list += "\n\t\t" + immunity + ",";
                }
                list += "\n\t}";
            }
            list += ",";
        }
        return list;
    }

    public static void RegisterTypes()
    {
        var foundTypes = SubTypeReflector<PokemonType>.FindSubTypeClasses();
        foreach (var pokemonType in foundTypes)
        {
            if (pokemonType != null)
            {
                PokemonTypes.Add(pokemonType.TypeId, pokemonType);
            }
        }
    }
}
