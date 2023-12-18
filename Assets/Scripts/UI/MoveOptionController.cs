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
        public List<ActiveMove> Moves => _playerController.Party.GetSelectedPokemon().Moves.ToList();
        public float OptionsEntryHeight = 55;
        private ListView _movesView;
        private VisualTreeAsset _movesEntry;

        public MoveOptionController(ListView movesView, VisualTreeAsset battleOptionEntry)
        {
            _movesView = movesView;
            _movesView.selectionType = SelectionType.None;
            _movesEntry = battleOptionEntry;
            InitializeMoveOptions();
        }

        public void ShowMoveOptions()
        {
            _movesView.itemsSource = Moves;
            _movesView.Rebuild();
            _movesView.style.display = DisplayStyle.Flex;
        }

        internal void HideMoveOptions()
        {
            _movesView.style.display = DisplayStyle.None;
        }

        private void InitializeMoveOptions()
        {
            _movesView.makeItem = () =>
            {
                var optionEntry = _movesEntry.Instantiate();
                return optionEntry;
            };

            _movesView.bindItem = (element, index) =>
            {
                var move = Moves[index];

                var moveNameLabel = element.Q("MoveNameLabel") as Label;
                var powerPointsLabel = element.Q("PowerPointsLabel") as Label;
                var button = element.Q("MoveContainer");
                void SelectMove(ClickEvent click)
                {
                    ServiceLocator.Instance.BattleManager.SelectMove(move);
                }
                button.RegisterCallback<ClickEvent>(SelectMove);
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
