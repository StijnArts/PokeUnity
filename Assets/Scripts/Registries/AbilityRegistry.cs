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
    public static Dictionary<string, @string> Abilities = new Dictionary<string, @string>();

    public static void RegisterAbility(string id, @string ability)
    {
        Abilities.Add(id, ability);
    }

    public static List<string> GetAbilityIds() => Abilities.Values.Select(ability => ability.Id).ToList();

    public static @string GetAbility(string id)
    {
        @string value;
        Abilities.TryGetValue(id, out value);
        if (value == null)
        {
            return Abilities["none"];
        }
        return value;
    }

    public static void RegisterAbilities()
    {
        var abilities = SubTypeReflector<@string>.FindSubTypeClasses();
        foreach (@string ability in abilities)
        {
            Abilities.Add(ability.Id, ability);
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
                    if (!string.IsNullOrEmpty(pokemonId.FormId) && species.Forms.ContainsKey(pokemonId.FormId))
                    {
                        var form = species.Forms[pokemonId.FormId];
                        if (form.HiddenAbility == null)
                        {
                            form.HiddenAbility = ability;
                        }
                        else
                        {
                            Debug.LogWarning("Attempted to overwrite an already set hidden Id with " + ability.Id + " during Id registration for " + species.Id + "\n" +
                                "Overwrites should be made with a PokemonOverwrites Class.");
                        }
                    } 
                    else if (species.HiddenAbility == null)
                    {
                        species.HiddenAbility = ability;
                    } else
                    {
                        Debug.LogWarning("Attempted to overwrite an already set hidden Id with " + ability.Id + " during Id registration for " + species.Id + "\n" +
                            "Overwrites should be made with a PokemonOverwrites Class.");
                    }
                }
            }
        }
    }
}