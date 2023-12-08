using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogTextButton : MonoBehaviour
{
    UIDocument buttonDocument;
    Button uiButton;

    private void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        if(buttonDocument == null )
        {
            Debug.Log("No button document found.");
        }

        uiButton = buttonDocument.rootVisualElement.Q("DialogTextButton") as Button;
        if(uiButton == null)
        {
            Debug.Log("No button was found.");
        }

        uiButton.RegisterCallback<ClickEvent>(OnButtonClick);
    }

    private void OnButtonClick(ClickEvent evt)
    {
        ServiceLocator.Instance.DialogManager.DisplayNextDialog();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
