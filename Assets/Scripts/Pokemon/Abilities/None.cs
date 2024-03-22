using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class None : Ability
{
    public None() : base("none", "", new List<PokemonIdentifier>(), new List<PokemonIdentifier>())
    {
        
    }
}
