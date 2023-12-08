using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Grass Encounter Table")]
public class GrassEncounterTable : ScriptableObject
{
    public PokemonSpawnEntry[] spawnEntries;

    public PokemonSpawnEntry pickFromSpawnEntries()
    {
        if(spawnEntries.Length > 0)
        {
            //TODO logic for picking a spawn entry here
        }
       return new PokemonSpawnEntry(1,"pikachu", 1,1);
    }

    internal void setEncounters(PokemonSpawnEntry[] pokemonSpawnEntries)
    {
        this.spawnEntries = pokemonSpawnEntries;
    }
}
