using Assets.Scripts.Battle;
using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class Ability : Effect
{
    public string AbilityName;
    public List<PokemonIdentifier> ReceiverPokemonIds;
    public List<PokemonIdentifier> HiddenReceiverPokemonIds;
    //should register observables on the pokemon when the pokemon is instantiated, and on the battle when the pokemon holding the ability enters the battle.

    public bool IsPermanent = false;
    public bool SuppressWeather = false;
    public bool IsBreakable = true;

    public Ability(string id, string abilityName, List<PokemonIdentifier> receiverPokemonIds, List<PokemonIdentifier> hiddenReceiverPokemonIds)
    {
        Id = id;
        AbilityName = abilityName;
        ReceiverPokemonIds = receiverPokemonIds;
        HiddenReceiverPokemonIds = hiddenReceiverPokemonIds;
    }

}
