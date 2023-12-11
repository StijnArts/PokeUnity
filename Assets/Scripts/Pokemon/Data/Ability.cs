using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class Ability
{
    public string AbilityName;
    public string AbilityId;
    public List<PokemonIdentifier> ReceiverPokemonIds;
    public List<PokemonIdentifier> HiddenReceiverPokemonIds;
    //should register observables on the pokemon when the pokemon is instantiated, and on the battle when the pokemon holding the ability enters the battle.
    
    public Ability(string id, string abilityName, List<PokemonIdentifier> receiverPokemonIds, List<PokemonIdentifier> hiddenReceiverPokemonIds)
    {
        AbilityId = id;
        AbilityName = abilityName;
        ReceiverPokemonIds = receiverPokemonIds;
        HiddenReceiverPokemonIds = hiddenReceiverPokemonIds;
    }
}
