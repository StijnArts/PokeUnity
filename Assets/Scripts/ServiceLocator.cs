using Assets.Scripts.Battle;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance;
    public DialogUIManager DialogUIManager => GetComponentInChildren<DialogUIManager>();
    public HudUiManager HudUiManager => GetComponentInChildren<HudUiManager>();
    public DialogManager DialogManager => GetComponentInChildren<DialogManager>();
    public BattleManager BattleManager => GetComponentInChildren<BattleManager>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
