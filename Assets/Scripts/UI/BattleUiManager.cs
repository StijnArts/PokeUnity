using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class BattleUiManager
    {
        HudUiManager _hudUiManager => ServiceLocator.Instance.HudUiManager;
        UIDocument _standardHudDocument => _hudUiManager.StandardHudDocument;
        BattlerWidgetController _selectedOpposingBattlerWidgetController;
        BattlerWidgetController _selectedTeammateBattlerWidgetController;
        BattleOptionsListController _battleOptionsController;
        MoveOptionController _moveOptionController;
        VisualElement _battleContainer => _standardHudDocument.rootVisualElement.Q("BattleContainer");
        PokemonIndividualData SelectedOpposingPokemon;

        private BattleOptionsListController _partyListController;
        public void StartBattle(PokemonIndividualData teammmate, PokemonIndividualData opposingPokemon)
        {
            _selectedOpposingBattlerWidgetController = new BattlerWidgetController(_standardHudDocument.rootVisualElement.Q("SelectedOpponent") as VisualElement,
                opposingPokemon);
            _selectedOpposingBattlerWidgetController = new BattlerWidgetController(_standardHudDocument.rootVisualElement.Q("SelectedTeammate") as VisualElement,
                teammmate);

            _moveOptionController = new MoveOptionController(_standardHudDocument.rootVisualElement.Q("MovesView") as ListView, _hudUiManager.MoveMenuEntry);
            _moveOptionController.HideMoveOptions();
            _battleOptionsController = new BattleOptionsListController(_standardHudDocument.rootVisualElement.Q("OptionsView") as ListView, 
                _hudUiManager.BattleMenuEntry, _moveOptionController.ShowMoveOptions);

            _battleContainer.style.display = DisplayStyle.Flex;

        }

        public void SetSelectedOpposingPokemon(PokemonIndividualData opposingPokemon)
        {

        }

        public void SetSelectedTeammate(PokemonIndividualData teammmate)
        {

        }
    }
}
