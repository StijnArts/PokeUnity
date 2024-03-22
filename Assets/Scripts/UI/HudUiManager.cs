using Assets.Scripts.Player;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HudUiManager : MonoBehaviour
{
    public UIDocument StandardHudDocument;
    public VisualTreeAsset PartyEntry;
    public VisualTreeAsset BattleMenuEntry;
    public VisualTreeAsset MoveMenuEntry;
    private PartyListController _partyListController;
    private void OnEnable()
    {
        
        StandardHudDocument = GetComponentInChildren<UIDocument>();
        if (StandardHudDocument == null)
        {
            Debug.Log("No Dialog UIDocument was found.");
        }
        var partyView = StandardHudDocument.rootVisualElement.Q("PartyView") as ListView;
        _partyListController = new PartyListController(partyView, PartyEntry);
    }

    public void RefreshPartyList()
    {
        _partyListController.RefreshPartyList();
    }
}
