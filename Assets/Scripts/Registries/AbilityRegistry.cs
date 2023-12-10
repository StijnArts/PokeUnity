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
            foreach(var pokemonId in ability.ReceiverPokemonIds)
            {
                PokemonSpecies species;
                if (PokemonRegistry.TryGetPokemon(pokemonId, out species)){
                    species.Abilities.Add(ability);
                }
            }

            foreach (var pokemonId in ability.HiddenReceiverPokemonIds)
            {
                PokemonSpecies species;
                if (PokemonRegistry.TryGetPokemon(pokemonId, out species))
                {
                    if(species.HiddenAbility == null)
                    {
                        species.HiddenAbility = ability;
                    } else
                    {
                        Debug.LogWarning("Attempted to overwrite an already set hidden ability with " + ability.AbilityId + " during ability registration for " + species.PokemonId + "\n" +
                            "Overwrites should be made with a PokemonOverwrites Class.");
                    }
                }
            }
        }

        return registry;
    }
}