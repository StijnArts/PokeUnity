using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance;
    public DialogUIManager DialogUIManager {  get; private set; }
    public DialogManager DialogManager { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DialogManager = GetComponentInChildren<DialogManager>();
        DialogUIManager = GetComponentInChildren<DialogUIManager>();
    }
}
