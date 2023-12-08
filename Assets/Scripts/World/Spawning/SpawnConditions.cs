using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Spawn Conditions")]
[Serializable]
public class SpawnConditions : ScriptableObject
{//Contains information on spawning (Shiny odds, if pokemon can have hidden ability and if the pokemon can be shiny
    public bool canHaveHiddenAbility = false;
    public bool canBeShiny = true;
    public int perfectIVs = 0;
    public Pokemon.Stats[] perfectIvs = new Pokemon.Stats[0];
    public bool hasSetIvs = false;
    public PokemonIVs setIvs = new PokemonIVs();
}
