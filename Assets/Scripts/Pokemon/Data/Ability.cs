using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class Ability : UnityEngine.Object
{
    public string AbilityName;
    public string AbilityId;
    public List<string> ReceiverPokemonIds;
    public List<string> HiddenReceiverPokemonIds;
    //should register observables on the pokemon when the pokemon is instantiated, and on the battle when the pokemon holding the ability enters the battle.
    
    public Ability(string id, string abilityName, List<string> receiverPokemonIds, List<string> hiddenReceiverPokemonIds)
    {
        AbilityId = id;
        AbilityName = abilityName;
        ReceiverPokemonIds = receiverPokemonIds;
        HiddenReceiverPokemonIds = hiddenReceiverPokemonIds;
    }
}
