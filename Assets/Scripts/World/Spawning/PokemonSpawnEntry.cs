using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PokemonSpawnEntry
{
    //Contains the pokemon id, form id, and level range 
    public string pokemonId = "";
    public string formId = "";
    public int minLevel = 0;
    public int maxLevel = 0;
    public float spawnWeight = 0;
    public Move MoveOne;
    public Move MoveTwo;
    public Move MoveThree;
    public Move MoveFour;
    public string AbilityId;
    public string Nickname;
    public PokemonSpawnEntry(float spawnWeight, string pokemonId, string formId, int minLevel, int maxLevel) : this(spawnWeight, pokemonId, minLevel, maxLevel)
    {
        this.formId = formId;
    }
    public PokemonSpawnEntry(float spawnWeight, string pokemonId, int minLevel, int maxLevel)
    {
        this.pokemonId = pokemonId;
        this.minLevel = minLevel;
        this.maxLevel = maxLevel;
        this.spawnWeight = spawnWeight;
    }

    public Move[] GetMoves()
    {
        return new Move[] { MoveOne, MoveTwo, MoveThree, MoveFour };
    }
}
