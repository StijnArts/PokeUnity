using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Pokemon.Species.Pikachu;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Static : Ability
{
    public Static() : base(
        "static", "Static",
        new List<PokemonIdentifier>()
        {
            Pikachu.Identifier
        }, 
        new List<PokemonIdentifier>())
    {
        
    }
}
