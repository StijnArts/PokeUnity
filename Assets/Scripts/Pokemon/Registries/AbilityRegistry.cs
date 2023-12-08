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
    public static Dictionary<Ability.Abilities, Ability> Abilities = new Dictionary<Ability.Abilities, Ability>();

    public static void RegisterPokemonAbility(Ability.Abilities id, Ability ability)
    {
        Abilities.Add(id, ability);
    }

    public static List<string> GetAbilityIds() => Abilities.Values.Select(ability => ability.AbilityId).ToList();

    public static Ability GetAbility(string id)
    {
        return GetAbility(GetAbilityEnum(id));
    }

    public static Ability GetAbility(Ability.Abilities id)
    {
        Ability value;
        Abilities.TryGetValue(id, out value);
        if (value == null)
        {
            return Abilities[Ability.Abilities.None];
        }
        return value;
    }

    public static Ability.Abilities GetAbilityEnum(string abilityName)
    {
        Ability.Abilities ability;
        if (Enum.TryParse(abilityName, true, out ability))
        {
            return ability;
        }
        return Ability.Abilities.None;
    }

    public static void RegisterAbilities()
    {
        var abilities = Registry<Ability>.FindRegistryChildClasses();
        foreach (Ability ability in abilities)
        {
            RegisterPokemonAbility(ability.AbilityEnum, ability);
        }
    }
}