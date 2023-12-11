using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PokemonCreator
{
    public static PokemonIndividualData InstantiatePokemonForSpawn(PokemonSpawnEntry pokemonSpawnEntry, SpawnConditions conditions)
    {
        //TODO make spawn conditions apply to the whole area where the pokemon is encountered, and so it can instead by overwritten by the spawn entry's spawn conditions
        PokemonSpecies pokemonSpecies = PokemonRegistry.GetPokemonSpecies(pokemonSpawnEntry.pokemonId);
        if (pokemonSpecies != null )
        {
            
            return putDataIntoPokemon(pokemonSpawnEntry, conditions, pokemonSpecies);
        }
        Debug.Log("Failed to find pokemon Data.");
        return new PokemonIndividualData();
    }

    private static PokemonIndividualData putDataIntoPokemon(PokemonSpawnEntry pokemonSpawnEntry, SpawnConditions conditions, PokemonSpecies pokemonSpecies)
    {
        PokemonIndividualData pokemonIndividualData = new PokemonIndividualData();
        if(pokemonSpawnEntry.formId == "")
        {
            pokemonIndividualData.pokemonName = pokemonSpecies.PokemonName;
            pokemonIndividualData.nationalPokedexNumber = pokemonSpecies.NationalPokedexNumber;
            pokemonIndividualData.primaryType = PokemonTypeRegistry.GetType(pokemonSpecies.PrimaryType);
            if (pokemonSpecies.SecondaryType != null)
            {
                pokemonIndividualData.secondaryType = PokemonTypeRegistry.GetType(pokemonSpecies.SecondaryType);
            }
        }
        //TODO fill with form information if it overrides base info
        pokemonIndividualData.Nature = (Nature.Natures)DetermineNature();
        pokemonIndividualData.level = new ObservableClasses.ObservableInteger() { Value = UnityEngine.Random.Range(pokemonSpawnEntry.minLevel, pokemonSpawnEntry.maxLevel + 1) };
        //abilities from range
        if(pokemonSpawnEntry.AbilityId == null)
        {
            pokemonIndividualData.Ability = GenerateAbility(pokemonSpecies, conditions);
        } else
        {
            pokemonIndividualData.Ability = AbilityRegistry.GetAbility(pokemonSpawnEntry.AbilityId);
        }

        pokemonIndividualData.AbilityData = pokemonIndividualData.Ability.AbilityId;
        pokemonIndividualData.IVs = GenerateIVs();
        pokemonIndividualData.gender = DetermineGender(pokemonSpecies.MaleRatio);
        pokemonIndividualData.friendship = pokemonSpecies.BaseFriendship;
        pokemonIndividualData.isValid = true;
        pokemonIndividualData.EVs = new PokemonEVs();
        pokemonIndividualData.PokemonId = pokemonSpawnEntry.pokemonId;
        pokemonIndividualData.FormId = pokemonSpawnEntry.formId;
        pokemonIndividualData.moves = DetermineMoves(pokemonSpawnEntry, pokemonSpecies);
        pokemonIndividualData.baseStats = new PokemonStats(pokemonSpecies.BaseStats);
        return pokemonIndividualData;

        //TODO give pokemon all learnable moves it should have for its level and give pokemon its 4 latest level up moves
    }

    private static Move[] DetermineMoves(PokemonSpawnEntry pokemonSpawnEntry, PokemonSpecies pokemonSpecies)
    {
        //TODO make movepool size adhere to global setting
        Move[] moves = new Move[4];

        if (pokemonSpawnEntry.moveOne != null){ moves[0] = pokemonSpawnEntry.moveOne;}
        if (pokemonSpawnEntry.moveTwo != null) { moves[1] = pokemonSpawnEntry.moveTwo;}
        if (pokemonSpawnEntry.moveThree != null) { moves[2] = pokemonSpawnEntry.moveThree;}
        if (pokemonSpawnEntry.moveFour != null) { moves[3] = pokemonSpawnEntry.moveFour;}

        //TODO fill remaining slots with moves from most recently learned movepool moves
        return moves;
    }

    private static Pokemon.PokemonGender DetermineGender(double maleRatio)
    {
        if(maleRatio < 0)
        {
            return Pokemon.PokemonGender.NONE;
        }
        float randomNumber = UnityEngine.Random.Range(0.0f, 1.0f);
        if(randomNumber < maleRatio)
        {
            return Pokemon.PokemonGender.MALE;
        } else
        {
            return Pokemon.PokemonGender.FEMALE; 
        }
    }

    private static int DetermineNature()
    {
        //TODO make it affected by abilities
        return UnityEngine.Random.Range(0, PokemonNatureRegistry.PokemonNatures.Count);
    }

    private static PokemonIVs GenerateIVs()
    {
        return new PokemonIVs(
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32));
    }

    private static Ability GenerateAbility(PokemonSpecies pokemonSpecies, SpawnConditions conditions)
    {
        var abilityPool = new List<Ability>(pokemonSpecies.Abilities);
        if(conditions.canHaveHiddenAbility == true)
        {
            abilityPool.Add(pokemonSpecies.HiddenAbility);
        }
        var selectedAbility = abilityPool[UnityEngine.Random.Range(0, abilityPool.Count)];
        return selectedAbility;
    }
}
