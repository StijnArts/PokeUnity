using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class PartyListController
    {
        public float PartyEntryHeight = 90;
        private PlayerParty _playerParty => GameObject.Find("Player").GetComponentInChildren<PlayerController>().Party;
        private List<PokemonIndividualData> _partyList => _playerParty.PartyAsList();
        private ListView _partyView;
        private VisualTreeAsset _partyEntry;

        public PartyListController(ListView partyView, VisualTreeAsset partyEntry)
        {
            _partyView = partyView;
            partyView.selectionType = SelectionType.None;
            _partyEntry = partyEntry;
            GameStateManager.CurrentGameState.OnChanged += InitializePartyListAfterLoading();
        }

        public void ShowPartyList()
        {
            _partyView.style.display = DisplayStyle.Flex;
        }

        internal void HideParty()
        {
            _partyView.style.display = DisplayStyle.None;
        }

        private void InitializePartyList()
        {
            AdjustPartyViewSize();
            _partyView.makeItem = () =>
            {
                var partyEntry = _partyEntry.Instantiate();
                partyEntry.userData = new PartyEntryController(partyEntry.Q<VisualElement>("PokemonSprite"));
                return partyEntry;
            };

            _partyView.bindItem = (element, index) =>
            {
                var partyPokemon = _playerParty.PartyPokemon[index];
                var controller = element.userData as PartyEntryController;
                controller.SetPokemonData(partyPokemon);

            };

            _partyView.fixedItemHeight = PartyEntryHeight;
            _partyView.itemsSource = _partyList;
        }

        public Action InitializePartyListAfterLoading()
        {
            return () =>
            {
                if (GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
                {
                    InitializePartyList();
                    GameStateManager.CurrentGameState.OnChanged -= InitializePartyListAfterLoading();
                }
            };
        }

        public void RefreshPartyList()
        {
            _partyView.itemsSource = _partyList;
            _partyView.Rebuild();
            AdjustPartyViewSize();
        }

        private void AdjustPartyViewSize()
        {
            var partyListSize = PartyEntryHeight * _partyList.Count;
            _partyView.style.minHeight = partyListSize;
            _partyView.style.maxHeight = partyListSize;
        }
    }
}
