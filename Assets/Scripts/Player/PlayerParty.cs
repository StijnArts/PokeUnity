using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerParty : Party
    {
        private PlayerController _player;
        private HudUiManager _hudUiManager => ServiceLocator.Instance.HudUiManager;
        public PlayerParty(PlayerController player) 
        { 
            _player = player;
        }
        public void AddPokemonToParty(PokemonIndividualData pokemonToAdd)
        {
            if (!IsPartyFull())
            {
                var slotNumber = getLastEmptySlot();
                PartyPokemon[slotNumber] = pokemonToAdd;

                _hudUiManager.RefreshPartyList();
            }

            //TODO send pokemon to pc
        }

        public bool IsPartyFull()
        {
            bool isPartyFull = true;
            foreach (var partySlot in PartyPokemon)
            {
                if (partySlot == null)
                {
                    isPartyFull = false;
                }
            }
            return isPartyFull;
        }

        public bool partySlotIsFilled(int slot)
        {
            return PartyPokemon[slot] != null;
        }

        public int getLastEmptySlot()
        {
            //TODO finish this method
            int slot = 0;
            bool emptySlotHasBeenFound = false;
            foreach (PokemonIndividualData pokemon in PartyPokemon)
            {
                if (pokemon == null)
                {
                    return slot;
                }
                slot++;
            }
            return slot;
        }

        public PokemonIndividualData getFirstPartySlot()
        {
            return PartyPokemon[0];
        }
    }
}
