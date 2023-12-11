using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonGiftEntry : PokemonSpawnEntry
{
    public string CustomFlavorText;
    public SpawnConditions spawnConditions;
    public PokemonGiftEntry(float spawnWeight, string pokemonId, string formId, int minLevel, int maxLevel) : base(spawnWeight, pokemonId, formId, minLevel, maxLevel)
    {
    }
}

    

