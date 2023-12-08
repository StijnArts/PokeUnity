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
            if (PokemonTypes[id].interactions.Count > 0)
            {
                list += "\n\tsuper effective on: { ";
                foreach (string weakness in PokemonTypes[id].interactions.Keys.Where(
                    key => PokemonTypes[id].interactions[key] == PokemonType.TypeInteraction.STRONG_AGAINST)
                    )
                {
                    list += "\n\t\t" + weakness + ",";
                }
                list += "\n\t},";
            
            
                list += "\n\tresisted by: { ";
                foreach (string resistance in PokemonTypes[id].interactions.Keys.Where(
                    key => PokemonTypes[id].interactions[key] == PokemonType.TypeInteraction.RESISTED_BY)
                    )
                {
                    list += "\n\t\t" + resistance + ",";
                }
                list += "\n\t},";
            
                list += "\n\ttypes with immunity: { ";
                foreach (string immunity in PokemonTypes[id].interactions.Keys.Where(
                    key => PokemonTypes[id].interactions[key] == PokemonType.TypeInteraction.HAS_IMMUNITY)
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

    public static void registerTypes()
    {   
        PokemonTypes.Add("normal", new PokemonType("normal", new List<string> { },  new List<string> { "rock", "steel"}, new List<string> { "ghost"}));
        PokemonTypes.Add("fighting", new PokemonType(
                                        "fighting",  //type name
                                        new List<string> {"dark","ice", "normal","rock","steel" }, //strong against
                                        new List<string> { "bug", "fairy", "flying", "poison", "psychic" }, //resisted by
                                        new List<string> { "ghost" })); //has immunities
        PokemonTypes.Add("flying", new PokemonType(
                                        "flying",  //type name
                                        new List<string> { "bug", "fighting", "grass" }, //strong against
                                        new List<string> { "electric", "rock", "steel" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("poison", new PokemonType(
                                        "poison",  //type name
                                        new List<string> { "fairy", "grass" }, //strong against
                                        new List<string> { "poison", "ground", "rock", "ghost"}, //resisted by
                                        new List<string> { "steel" })); //has immunities
        PokemonTypes.Add("ground", new PokemonType(
                                        "ground",  //type name
                                        new List<string> { "electric", "fire", "poison", "rock", "steel" }, //strong against
                                        new List<string> { "bug", "grass" }, //resisted by
                                        new List<string> { "flying" })); //has immunities
        PokemonTypes.Add("rock", new PokemonType(
                                        "rock",  //type name
                                        new List<string> { "bug", "fire", "flying", "ice" }, //strong against
                                        new List<string> { "fight", "ground", "steel" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("bug", new PokemonType(
                                        "bug",  //type name
                                        new List<string> { "dark", "grass", "psychic" }, //strong against
                                        new List<string> { "fairy", "fight", "fire", "flying", "ghost", "poison", "steel" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("ghost", new PokemonType(
                                        "ghost",  //type name
                                        new List<string> { "ghost", "psychic" }, //strong against
                                        new List<string> { "dark" }, //resisted by
                                        new List<string> { "normal" })); //has immunities
        PokemonTypes.Add("steel", new PokemonType(
                                        "steel",  //type name
                                        new List<string> { "fairy", "ice", "rock" }, //strong against
                                        new List<string> { "electric", "fire", "steel", "water" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("???", new PokemonType(
                                        "???",  //type name
                                        new List<string> { }, //strong against
                                        new List<string> { }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("fire", new PokemonType(
                                        "fire",  //type name
                                        new List<string> { "bug", "grass", "ice", "steel" }, //strong against
                                        new List<string> { "dragon", "fire", "rock", "water" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("water", new PokemonType(
                                        "water",  //type name
                                        new List<string> { "fire", "ground", "rock" }, //strong against
                                        new List<string> { "dragon", "grass", "water" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("grass", new PokemonType(
                                        "grass",  //type name
                                        new List<string> { "ground", "rock", "water" }, //strong against
                                        new List<string> { "bug", "dragon", "fire", "flying", "grass", "poison", "steel" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("electric", new PokemonType(
                                        "electric",  //type name
                                        new List<string> { "flying", "water" }, //strong against
                                        new List<string> { "dragon", "electric", "grass" }, //resisted by
                                        new List<string> { "ground" })); //has immunities
        PokemonTypes.Add("psychic", new PokemonType(
                                        "psychic",  //type name
                                        new List<string> { "fight", "poison" }, //strong against
                                        new List<string> { "psychic" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("ice", new PokemonType(
                                        "ice",  //type name
                                        new List<string> { "dragon", "flying", "grass", "ground" }, //strong against
                                        new List<string> { "fire", "ice","steel", "water" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("dragon", new PokemonType(
                                        "dragon",  //type name
                                        new List<string> { "dragon", "psychic" }, //strong against
                                        new List<string> { "steel" }, //resisted by
                                        new List<string> { "fairy" })); //has immunities
        PokemonTypes.Add("dark", new PokemonType(
                                        "dark",  //type name
                                        new List<string> { "ghost", "psychic" }, //strong against
                                        new List<string> { "dark", "fairy", "fight" }, //resisted by
                                        new List<string> { })); //has immunities
        PokemonTypes.Add("fairy", new PokemonType(
                                        "fairy",  //type name
                                        new List<string> { "dark", "dragon", "fight" }, //strong against
                                        new List<string> { "fire", "poison", "steel" }, //resisted by
                                        new List<string> { })); //has immunities
    }
}
