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
        public int SelectedPokemon;
        private HudUiManager _hudUiManager => ServiceLocator.Instance.HudUiManager;
        public PlayerParty(PlayerController player) 
        { 
            _player = player;
        }
        public void AddPokemonToParty(PokemonIndividualData pokemonToAdd)
        {
            if (!IsPartyFull())
            {
                var partyIsEmpty = IsPartyEmpty();
                var slotNumber = GetLastEmptySlot();
                PartyPokemon[slotNumber] = pokemonToAdd;
                if (partyIsEmpty)
                {
                    SelectedPokemon = slotNumber;
                }
                _hudUiManager.RefreshPartyList();
            }

            //TODO send pokemon to pc
        }

        private bool IsPartyEmpty()
        {
            bool isPartyEmpty = true;
            foreach (var partySlot in PartyPokemon)
            {
                if (partySlot != null)
                {
                    isPartyEmpty = false;
                }
            }
            return isPartyEmpty;
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

        public int GetLastEmptySlot()
        {
            //TODO finish this method
            int slot = 0;
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

        public PokemonIndividualData GetFirstPartySlot()
        {
            return PartyPokemon[0];
        }

        public PokemonIndividualData GetSelectedPokemon()
        {
            return PartyPokemon[SelectedPokemon];
        }

        public List<PokemonIndividualData> PartyAsList()
        {
            var selectedPokemon = new List<PokemonIndividualData>();
            foreach (var partyPokemon in PartyPokemon)
            {
                if (partyPokemon != null)
                {
                    selectedPokemon.Add(partyPokemon);
                }
            }
            return selectedPokemon;
        }
    }
}
