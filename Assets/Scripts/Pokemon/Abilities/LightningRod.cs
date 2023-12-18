using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Pokemon.Species.Pikachu;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightningRod : @string
{
    public LightningRod() : base(
        "lightning_rod", "Lightning Rod",
        new List<PokemonIdentifier>()
        {}, 
        new List<PokemonIdentifier>()
        {
            Pikachu.Identifier
        })
    {
        
    }
}
