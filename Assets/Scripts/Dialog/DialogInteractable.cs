using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DialogInteractable : MonoBehaviour
{
    [SerializeField]
    public string interactableName;
    [SerializeField]
    public Dialog[] dialogs = new Dialog[0];

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().EnterDialogRange(this);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().ExitDialogRange(this);
        }
    }

    private bool flagsAreMet()
    {
        throw new NotImplementedException();
    }

    public void StartDialog()
    {
        //If no flags are met say default dialog.
        //If multiple sets of dialog have 
        foreach (var dialog in dialogs)
        {
            if(dialog.dialogTitle == null)
            {
                dialog.dialogTitle = interactableName;
            } else if(dialog.dialogTitle == "")
            {
                dialog.dialogTitle = interactableName;
            }
        }
        ServiceLocator.Instance.DialogManager.StartDialog(dialogs);
    }
}
