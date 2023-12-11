using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HudUiManager : MonoBehaviour
{
    UIDocument DialogUIDocument;
    ListView PartyView;
    public VisualTreeAsset PartyEntry;
    private PlayerParty _playerParty => GameObject.Find("Player").GetComponentInChildren<PlayerController>().Party;
    private void OnEnable()
    {
        DialogUIDocument = GetComponentInChildren<UIDocument>();
        if (DialogUIDocument == null)
        {
            Debug.Log("No Dialog UIDocument was found.");
        }
        PartyView = DialogUIDocument.rootVisualElement.Q("PartyView") as ListView;
        GameStateManager.currentGameState.OnChanged += InitializePartyListAfterLoading();
    }

    public void ShowDialogBox()
    {
        PartyView.style.display = DisplayStyle.Flex;
    }

    internal void HideParty()
    {
        PartyView.style.display = DisplayStyle.None;
    }

    private void InitializePartyList()
    {
        PartyView.makeItem = () =>
        {
            var partyEntry = PartyEntry.Instantiate();
            partyEntry.userData = new PartyEntryController(partyEntry.Q<VisualElement>("PokemonSprite"));
            return partyEntry;
        };

        PartyView.bindItem = (element, index) =>
        {
            var controller = element.userData as PartyEntryController;
            controller.SetPokemonData(_playerParty.PartyPokemon[index]);
        };

        PartyView.fixedItemHeight = 90;

        PartyView.itemsSource = _playerParty.PartyPokemon;
    }

    private Action InitializePartyListAfterLoading()
    {
        return () =>
        {
            if (GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
            {
                InitializePartyList();
                GameStateManager.currentGameState.OnChanged -= InitializePartyListAfterLoading();
            }
        };
    }

    public void RefreshPartyList()
    {
        PartyView.Rebuild();
    }
}
