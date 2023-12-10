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

    public static void RegisterAbility(string id, Ability ability)
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
            //TODO check for any modification files that target the ability and apply them
            foreach(var pokemonId in ability.ReceiverPokemonIds)
            {
                PokemonSpecies species;
                if (PokemonRegistry.TryGetPokemon(pokemonId.SpeciesId, out species)){
                    if(!string.IsNullOrEmpty(pokemonId.FormId) && species.Forms.ContainsKey(pokemonId.FormId))
                    {
                        species.Forms[pokemonId.FormId].Abilities.Add(ability);
                    } else
                    {
                        species.Abilities.Add(ability);
                    }
                }
            }

            foreach (var pokemonId in ability.HiddenReceiverPokemonIds)
            {
                PokemonSpecies species;
                if (PokemonRegistry.TryGetPokemon(pokemonId.SpeciesId, out species))
                {
                    var targetSpecies = species;
                    if (!string.IsNullOrEmpty(pokemonId.FormId) && species.Forms.ContainsKey(pokemonId.FormId))
                    {
                        targetSpecies = species.Forms[pokemonId.FormId];
                    }
                    if (targetSpecies.HiddenAbility == null)
                    {
                        targetSpecies.HiddenAbility = ability;
                    } else
                    {
                        Debug.LogWarning("Attempted to overwrite an already set hidden AbilityId with " + ability.AbilityId + " during AbilityId registration for " + species.PokemonId + "\n" +
                            "Overwrites should be made with a PokemonOverwrites Class.");
                    }
                }
            }
        }

        return registry;
    }
}