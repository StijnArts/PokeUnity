using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Party
{
    public PokemonIndividualData[] PartyPokemon = new PokemonIndividualData[Settings.MaxPartySize-1];

    public Party()
    {

    }

    public Party(int partySize)
    {
        PartyPokemon = new PokemonIndividualData[partySize];
    }
}
