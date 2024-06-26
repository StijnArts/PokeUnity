﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
public abstract class PokemonType
{
    //Interactions are stored one way to make it easier to edit types.
    public enum TypeInteraction {STRONG_AGAINST, RESISTED_BY, HAS_IMMUNITY, NEUTRAL};
    public string TypeId;
    public Dictionary<string, TypeInteraction> Interactions = new Dictionary<string, TypeInteraction>();
    public PokemonType(string pokemonTypeName, List<string> strongAgainst, List<string> resistedBy, List<string> hasImmunityAgainst = null)
    {
        TypeId = pokemonTypeName;
        foreach (string type in strongAgainst)
        {
            Interactions.Add(type, TypeInteraction.STRONG_AGAINST);
        }
        foreach (string type in resistedBy)
        {
            Interactions.Add(type, TypeInteraction.RESISTED_BY);
        }
        if(hasImmunityAgainst != null)
        {
            foreach (string type in hasImmunityAgainst)
            {
                Interactions.Add(type, TypeInteraction.HAS_IMMUNITY);
            }
        }
    }
    //returns how this type interacts with the other type when dealing damage.
    public TypeInteraction getTypeInteraction(string otherType)
    {
        if (Interactions.ContainsKey(otherType))
        {
            return Interactions[otherType];
        }
        else
        {
            return TypeInteraction.NEUTRAL;
        }
    }
}