using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class BattleOptionsListController
    {
        public enum BattleOption { Battle, Pokemon, Bag, Run }
        public List<BattleOption> Options = new List<BattleOption>(Enum.GetValues(typeof(BattleOption)).Cast<BattleOption>());
        public float OptionsEntryHeight = 55;
        private ListView _optionsView;
        private VisualTreeAsset _optionsEntry;
        private Action _showMoveOptions;
        public BattleOptionsListController(ListView optionsView, VisualTreeAsset battleOptionEntry, Action showMoveOptions)
        {
            _optionsView = optionsView.Q("OptionsView") as ListView;
            _optionsView.selectionType = SelectionType.None;
            _optionsEntry = battleOptionEntry;
            _showMoveOptions = showMoveOptions;
            InitializeBattleOptions(_showMoveOptions);
        }

        public void ShowBattleOptions()
        {
            _optionsView.style.display = DisplayStyle.Flex;
        }

        internal void HideBattleOptions()
        {
            _optionsView.style.display = DisplayStyle.None;
        }

        private void InitializeBattleOptions(Action showMoveOptions)
        {
            _optionsView.makeItem = () =>
            {
                var optionEntry = _optionsEntry.Instantiate();
                return optionEntry;
            };

            _optionsView.bindItem = (element, index) =>
            {
                var option = Options[index];

                var label = element.Q("OptionNameLabel") as Label;
                var button = element.Q("OptionContainer") as VisualElement;
                switch (option)
                {
                    case BattleOption.Battle:
                        {
                            void SwitchMenus(ClickEvent click)
                            {
                                _optionsView.style.display = DisplayStyle.None;
                                showMoveOptions();
                            }
                            button.RegisterCallback<ClickEvent>(SwitchMenus);
                        }
                        break;
                    case BattleOption.Bag:
                        {

                        }
                        break;
                    case BattleOption.Pokemon:
                        {

                        }
                        break;
                    case BattleOption.Run:
                        {

                        } break;
                }
                label.text = Enum.GetName(typeof(BattleOption), option).ToLower().FirstCharacterToUpper();

            };

            _optionsView.fixedItemHeight = OptionsEntryHeight;
            _optionsView.itemsSource = Options;
            AdjustOptionsViewSize();
        }

        public Action InitializeBattleOptionsAfterLoading()
        {
            return () =>
            {
                if (GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
                {
                    InitializeBattleOptions(_showMoveOptions);
                    GameStateManager.CurrentGameState.OnChanged -= InitializeBattleOptionsAfterLoading();
                }
            };
        }

        private void AdjustOptionsViewSize()
        {
            var partyListSize = OptionsEntryHeight * Options.Count;
            _optionsView.style.minHeight = partyListSize;
            _optionsView.style.maxHeight = partyListSize;
        }
    }
}
