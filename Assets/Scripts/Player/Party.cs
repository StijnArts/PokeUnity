using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Party
{
    public PokemonIndividualData[] party = new PokemonIndividualData[Settings.MaxPartySize];
    public void AddPokemonToParty(PokemonIndividualData pokemonToAdd)
    {
        if (!IsPartyFull())
        {
            party[getLastEmptySlot()] = pokemonToAdd;
        }
        //TODO send pokemon to pc
    }

    public bool IsPartyFull()
    {
        bool isPartyFull = true;
        foreach (var partySlot in party)
        {
            if ( partySlot == null)
            {
                isPartyFull = false;    
            }
        }
        return isPartyFull;
    }

    public bool partySlotIsFilled(int slot)
    {
        return party[slot] != null;
    }

    public int getLastEmptySlot()
    {
        //TODO finish this method
        int slot = 0;
        bool emptySlotHasBeenFound = false;
        foreach (PokemonIndividualData pokemon in party)
        {
            if(pokemon == null)
            {
                return slot;
            }
            slot++;
        }
        return slot;
    }

    public PokemonIndividualData getFirstPartySlot()
    {
        return party[0];
    }
}
