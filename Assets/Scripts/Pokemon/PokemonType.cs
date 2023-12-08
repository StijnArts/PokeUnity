using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PokemonType
{
    //Interactions are stored one way to make it easier to edit types.
    public enum TypeInteraction {STRONG_AGAINST, RESISTED_BY, HAS_IMMUNITY, NEUTRAL};
    public string PokemonTypeName;
    public Dictionary<string, TypeInteraction> interactions = new Dictionary<string, TypeInteraction>();
    public PokemonType(string pokemonTypeName, List<string> strongAgainst, List<string> resistedBy, List<string> hasImmunityAgainst)
    {
        PokemonTypeName = pokemonTypeName;
        foreach (string type in strongAgainst)
        {
            interactions.Add(type, TypeInteraction.STRONG_AGAINST);
        }
        foreach (string type in resistedBy)
        {
            interactions.Add(type, TypeInteraction.RESISTED_BY);
        }
        foreach (string type in hasImmunityAgainst)
        {
            interactions.Add(type, TypeInteraction.HAS_IMMUNITY);
        }
    }
    //returns how this type interacts with the other type when dealing damage.
    public TypeInteraction getTypeInteraction(string otherType)
    {
        if (interactions.ContainsKey(otherType))
        {
            return interactions[otherType];
        }
        else
        {
            return TypeInteraction.NEUTRAL;
        }
    }
}