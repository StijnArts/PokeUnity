using Assets.Scripts.Pokemon;
using Assets.Scripts.Registries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            pokemonIndividualData.PokemonName = pokemonSpecies.PokemonName;
            pokemonIndividualData.PrimaryType = PokemonTypeRegistry.GetType(pokemonSpecies.PrimaryType);
            if (pokemonSpecies.SecondaryType != null)
            {
                pokemonIndividualData.SecondaryType = PokemonTypeRegistry.GetType(pokemonSpecies.SecondaryType);
            }
        }
        //TODO fill with form information if it overrides base info
        pokemonIndividualData.Nature = (Nature.Natures)DetermineNature();
        pokemonIndividualData.Level = new ObservableClasses.ObservableInteger() { Value = UnityEngine.Random.Range(pokemonSpawnEntry.minLevel, pokemonSpawnEntry.maxLevel + 1) };
        //abilities from range
        if(string.IsNullOrWhiteSpace(pokemonSpawnEntry.AbilityId))
        {
            pokemonIndividualData.Ability = GenerateAbility(pokemonSpecies, conditions);
        } else
        {
            pokemonIndividualData.Ability = AbilityRegistry.GetAbility(pokemonSpawnEntry.AbilityId);
        }

        pokemonIndividualData.AbilityData = pokemonIndividualData.Ability.Id;
        pokemonIndividualData.IVs = GenerateIVs();
        pokemonIndividualData.gender = DetermineGender(pokemonSpecies.MaleRatio);
        pokemonIndividualData.friendship = pokemonSpecies.BaseFriendship;
        pokemonIndividualData.isValid = true;
        pokemonIndividualData.EVs = new PokemonEVs();
        pokemonIndividualData.PokemonId = pokemonSpawnEntry.pokemonId;
        pokemonIndividualData.FormId = pokemonSpawnEntry.formId;
        pokemonIndividualData.Moves = DetermineMoves(pokemonSpawnEntry, pokemonSpecies, pokemonIndividualData.Level.Value);
        pokemonIndividualData.learnableMoves = DetermineLearnableMoves(pokemonSpawnEntry, pokemonSpecies, pokemonIndividualData.Level.Value);
        pokemonIndividualData.BaseStats = new PokemonStats(pokemonSpecies.BaseStats);
        pokemonIndividualData.Nickname = pokemonSpawnEntry.Nickname;
        pokemonIndividualData.Initialize();

        return pokemonIndividualData;

        //TODO give pokemon all learnable moves it should have for its level and give pokemon its 4 latest level up moves
    }

    private static List<Move> DetermineLearnableMoves(PokemonSpawnEntry pokemonSpawnEntry, PokemonSpecies pokemonSpecies, int value)
    {
        var learnableMoves = new List<Move>();
        var learnedMoves = pokemonSpecies.MoveSets[0].LevelUpMoves.Where(keyValuePair => keyValuePair.Key <= value).Select(keyValuePair => MoveRegistry.GetMove(keyValuePair.Value)).ToList();
        learnableMoves.AddRange(learnedMoves);
        var spawnMoves = pokemonSpawnEntry.GetMoves();
        learnableMoves.AddRange(spawnMoves);
        return learnableMoves;
    }

    private static ActiveMove[] DetermineMoves(PokemonSpawnEntry pokemonSpawnEntry, PokemonSpecies pokemonSpecies, int level)
    {
        //TODO make movepool size adhere to global setting
        Move[] moves = new Move[4];
        var spawnEntryMoves = pokemonSpawnEntry.GetMoves();
        if (pokemonSpawnEntry.MoveOne != null){ moves[0] = spawnEntryMoves[0];}
        if (pokemonSpawnEntry.MoveTwo != null) { moves[1] = spawnEntryMoves[1];}
        if (pokemonSpawnEntry.MoveThree != null) { moves[2] = spawnEntryMoves[2];}
        if (pokemonSpawnEntry.MoveFour != null) { moves[3] = spawnEntryMoves[3];}

        //TODO fill remaining slots with moves from most recently learned movepool moves
        var latestLevelUpMoves = pokemonSpecies.FindLatestLevelUpMoves(level).ToList();
        int moveCursor = 0;
        foreach (Move move in moves)
        {
            if(move == null)
            {
                foreach (Move learnSetMove in latestLevelUpMoves)
                {
                    if (!moves.Contains(learnSetMove))
                    {
                        moves[moveCursor] = learnSetMove;
                        latestLevelUpMoves.Remove(learnSetMove);
                        break;
                    }
                }
            }
            moveCursor++;
        }
        return moves.Where(move => move != null).Select(move => new ActiveMove(move)).ToArray();
    }

    private static PokemonNpc.PokemonGender DetermineGender(double maleRatio)
    {
        if(maleRatio < 0)
        {
            return PokemonNpc.PokemonGender.NONE;
        }
        float randomNumber = UnityEngine.Random.Range(0.0f, 1.0f);
        if(randomNumber < maleRatio)
        {
            return PokemonNpc.PokemonGender.MALE;
        } else
        {
            return PokemonNpc.PokemonGender.FEMALE; 
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
