using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class AbilityRegistry
{
    //TODO create abilities
    public static Dictionary<string, Ability> Abilities = RegisterAbilities();

    public static void RegisterPokemonAbility(string id, Ability ability)
    {
        Abilities.Add(id, ability);
    }

    public static List<string> GetAbilityIds() => Abilities.Values.Select(ability => ability.AbilityId).ToList();

    public static Ability GetAbility(string id)
    {
        Ability value;
        Abilities.TryGetValue(id, out value);
        if (value == null)
        {
            return Abilities["none"];
        }
        return value;
    }

    public static Dictionary<string, Ability> RegisterAbilities()
    {
        var registry = new Dictionary<string, Ability>();
        var abilities = SubTypeReflector<Ability>.FindSubTypeClasses();
        foreach (Ability ability in abilities)
        {
            registry.Add(ability.AbilityId, ability);
        }

        return registry;
    }
}