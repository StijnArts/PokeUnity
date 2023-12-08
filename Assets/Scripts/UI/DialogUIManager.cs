using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogUIManager : MonoBehaviour
{
    UIDocument dialogUIDocument;
    Label dialogTextLabel;
    VisualElement dialogContainer;
    private void OnEnable()
    {
        dialogUIDocument = GetComponentInChildren<UIDocument>();
        if(dialogUIDocument == null ) 
        {
            Debug.Log("No Dialog UIDocument was found.");
        }

        dialogTextLabel = dialogUIDocument.rootVisualElement.Q("DialogText") as Label;
        dialogContainer = dialogUIDocument.rootVisualElement.Q("DailogBox") as VisualElement;
    }
    public void showDialogBox()
    {
        dialogContainer.style.display = DisplayStyle.Flex;
    }

    public void setDialogText(String text)
    {
        dialogTextLabel.text = text;
    }

    internal void hideDialogBox()
    {
        dialogContainer.style.display = DisplayStyle.None;
    }

    internal void hideTitleAndPortrait()
    {
        //TODO hide the title and portrait
    }
}
