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
        PokemonData pokemonData = PokemonRegistry.GetPokemon(pokemonSpawnEntry.pokemonId);
        if (pokemonData != null )
        {
            
            return putDataIntoPokemon(pokemonSpawnEntry, conditions, pokemonData);
        }
        Debug.Log("Failed to find pokemon Data.");
        return new PokemonIndividualData();
    }

    private static PokemonIndividualData putDataIntoPokemon(PokemonSpawnEntry pokemonSpawnEntry, SpawnConditions conditions, PokemonData pokemonData)
    {
        PokemonIndividualData pokemonIndividualData = new PokemonIndividualData();
        if(pokemonSpawnEntry.formId == "")
        {
            pokemonIndividualData.pokemonName = pokemonData.PokemonName;
            pokemonIndividualData.nationalPokedexNumber = pokemonData.NationalPokedexNumber;
            pokemonIndividualData.primaryType = PokemonTypeRegistry.GetType(pokemonData.PrimaryType);
            if (pokemonData.SecondaryType != null)
            {
                pokemonIndividualData.secondaryType = PokemonTypeRegistry.GetType(pokemonData.SecondaryType);
            }
        }
        //TODO fill with form information if it overrides base info
        pokemonIndividualData.nature = (Nature.Natures)determineNature();
        pokemonIndividualData.level = new ObservableClasses.ObservableInteger() { Value = UnityEngine.Random.Range(pokemonSpawnEntry.minLevel, pokemonSpawnEntry.maxLevel + 1) };
        //abilities from range
        if(pokemonSpawnEntry.ability == null)
        {
            pokemonIndividualData.Ability = generateAbility(pokemonData, conditions);
        } else
        {
            pokemonIndividualData.Ability = pokemonSpawnEntry.ability;
        }
        
        pokemonIndividualData.IVs = generateIVs();
        pokemonIndividualData.gender = determineGender(pokemonData.MaleRatio);
        pokemonIndividualData.friendship = pokemonData.BaseFriendship;
        pokemonIndividualData.isValid = true;
        pokemonIndividualData.EVs = new PokemonEVs();
        pokemonIndividualData.pokemonId = pokemonSpawnEntry.pokemonId;
        pokemonIndividualData.formId = pokemonSpawnEntry.formId;
        pokemonIndividualData.moves = determineMoves(pokemonSpawnEntry, pokemonData);
        pokemonIndividualData.baseStats = new PokemonStats( pokemonData.BaseStats);
        return pokemonIndividualData;

        //TODO give pokemon all learnable moves it should have for its level and give pokemon its 4 latest level up moves
    }

    private static Move[] determineMoves(PokemonSpawnEntry pokemonSpawnEntry, PokemonData pokemonData)
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

    private static Pokemon.PokemonGender determineGender(double maleRatio)
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

    private static int determineNature()
    {
        //TODO make it affected by abilities
        return UnityEngine.Random.Range(0, PokemonNatureRegistry.PokemonNatures.Count);
    }

    private static PokemonIVs generateIVs()
    {
        return new PokemonIVs(
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32),
            UnityEngine.Random.Range(0, 32));
    }

    private static string generateAbility(PokemonData pokemonData, SpawnConditions conditions)
    {
        var abilityPool = new List<Ability.Abilities>(pokemonData.Abilities);
        if(conditions.canHaveHiddenAbility == true)
        {
            abilityPool.Add(pokemonData.HiddenAbility);
        }
        var selectedAbility = abilityPool[UnityEngine.Random.Range(0, abilityPool.Count)];
        return AbilityRegistry.GetAbility(selectedAbility).AbilityId;
    }
}
