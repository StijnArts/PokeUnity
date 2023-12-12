using Assets.Scripts.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class MoveOptionController
    {
        public enum BattleOption { Battle, Pokemon, Bag, Run }
        private PlayerController _playerController => GameObject.Find("Player").GetComponentInChildren<PlayerController>();
        public List<PokemonMove> Moves => _playerController.Party.GetSelectedPokemon().Moves.ToList();
        public float OptionsEntryHeight = 55;
        private VisualElement _optionsContainer;
        private ListView _movesView;
        private VisualTreeAsset _optionsEntry;

        public MoveOptionController(VisualElement optionsContainer, VisualTreeAsset battleOptionEntry)
        {
            _optionsContainer = optionsContainer;
            _movesView = optionsContainer.Q("MovesView") as ListView;
            _movesView.selectionType = SelectionType.None;
            _optionsEntry = battleOptionEntry;
            InitializeMoveOptions();
        }

        public void ShowMoveOptions()
        {
            _movesView.itemsSource = Moves;
            _movesView.Rebuild();
            _optionsContainer.style.display = DisplayStyle.Flex;
            
        }

        internal void HideMoveOptions()
        {
            _optionsContainer.style.display = DisplayStyle.None;
        }

        private void InitializeMoveOptions()
        {
            _movesView.makeItem = () =>
            {
                var optionEntry = _optionsEntry.Instantiate();
                return optionEntry;
            };

            _movesView.bindItem = (element, index) =>
            {
                var move = Moves[index];

                var moveNameLabel = element.Q("MoveNameLabel") as Label;
                var powerPointsLabel = element.Q("PowerPointsLabel") as Label;
                var button = element.Q("MoveContainer") as VisualElement;
                button.AddManipulator(new Clickable(evt => ServiceLocator.Instance.BattleManager.SelectMove(move)));
                moveNameLabel.text = move.Move.MoveName.ToLower().FirstCharacterToUpper();
                powerPointsLabel.text = move.RemainingPowerPoints + "/" + move.Move.PowerPoints;

            };

            _movesView.fixedItemHeight = OptionsEntryHeight;
            _movesView.itemsSource = Moves;
            AdjustOptionsViewSize();
        }

        public Action InitializeBattleOptionsAfterLoading()
        {
            return () =>
            {
                if (GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
                {
                    InitializeMoveOptions();
                    GameStateManager.CurrentGameState.OnChanged -= InitializeBattleOptionsAfterLoading();
                }
            };
        }

        private void AdjustOptionsViewSize()
        {
            var partyListSize = OptionsEntryHeight * Moves.Count;
            _movesView.style.minHeight = partyListSize;
            _movesView.style.maxHeight = partyListSize;
        }
    }
}
