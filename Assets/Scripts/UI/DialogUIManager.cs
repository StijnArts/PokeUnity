using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogUIManager : MonoBehaviour
{
    UIDocument dialogUIDocument;
    Label DialogTextLabel;
    VisualElement DialogContainer;
    private void OnEnable()
    {
        dialogUIDocument = GetComponentInChildren<UIDocument>();
        if(dialogUIDocument == null ) 
        {
            Debug.Log("No Dialog UIDocument was found.");
        }

        DialogTextLabel = dialogUIDocument.rootVisualElement.Q("DialogText") as Label;
        DialogContainer = dialogUIDocument.rootVisualElement.Q("DailogBox") as VisualElement;
    }
    public void ShowDialogBox()
    {
        DialogContainer.style.display = DisplayStyle.Flex;
    }

    public void SetDialogText(String text)
    {
        DialogTextLabel.text = text;
    }

    internal void HideDialogBox()
    {
        DialogContainer.style.display = DisplayStyle.None;
    }

    internal void HideTitleAndPortrait()
    {
        //TODO hide the title and portrait
    }
}
