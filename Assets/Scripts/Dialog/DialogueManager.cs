using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<Dialog> dialogQueue;
    private Queue<PokemonGiftEntry> pokemonGiftEntries;
    private DialogUIManager dialogUIManager;
    private Dialog previousDialog;
    private bool isGiftingPokemon;
    // Start is called before the first frame update
    void Start()
    {
        dialogQueue = new Queue<Dialog>();
        pokemonGiftEntries = new Queue<PokemonGiftEntry>();
        dialogUIManager = ServiceLocator.Instance.DialogUIManager;
        if (dialogUIManager == null)
        {
            Debug.Log("Did not find DialogUIManager");
        }
    }

    public void StartDialog(Dialog[] dialogs)
    {
        Debug.Log("Starting Conversation");
        CleanDialog();

        foreach (Dialog dialog in dialogs)
        {
            dialogQueue.Enqueue(dialog);
        }

        DisplayNextDialog();
    }

    private void CleanDialog()
    {
        dialogQueue.Clear();
        dialogUIManager.setDialogText("");
    }

    public void DisplayNextDialog()
    {
        if (previousDialog != null)
        {
            if (!previousDialog.actionsHaveBeenExecuted)
            {
                executeDialogAction(previousDialog);
                previousDialog.actionsHaveBeenExecuted = true;
            }
        }
        if (pokemonGiftEntries.Count > 0)
        {
            GiftPokemon(pokemonGiftEntries.Dequeue(), previousDialog.dialogTitle);
            //display pokemon gifting dialog from queue
            return;
        }
        if (allQueuesAreEmpty())
        {
            EndDialog();
            return;
        }

        Dialog dialog = dialogQueue.Dequeue();
        dialogUIManager.setDialogText(dialog.text);
        dialogUIManager.ShowDialogBox();
        Debug.Log(dialog.dialogTitle + ": " + dialog.text);
        if(previousDialog != null)
        {
            previousDialog.actionsHaveBeenExecuted = false;
        }
        previousDialog = dialog;
    }

    private bool allQueuesAreEmpty()
    {
        if(dialogQueue.Count == 0 && pokemonGiftEntries.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void GiftPokemon(PokemonGiftEntry pokemonGiftEntry, string source)
    {
        string flavorText = Settings.playerName + " received " + PokemonRegistry.GetPokemonSpecies(pokemonGiftEntry.pokemonId).PokemonName + " from " + source + "!";
        if (pokemonGiftEntry.customFlavorText != null)
        {
            if (pokemonGiftEntry.customFlavorText != "")
            {
                flavorText = pokemonGiftEntry.customFlavorText;
            }
        }
        Debug.Log("system: " + flavorText);
        dialogUIManager.hideTitleAndPortrait();
        dialogUIManager.setDialogText(flavorText);
        dialogUIManager.ShowDialogBox();
        PlayerController.Party.AddPokemonToParty(PokemonCreator.InstantiatePokemonForSpawn(pokemonGiftEntry, pokemonGiftEntry.spawnConditions));
    }

    private void executeDialogAction(Dialog dialog)
    {
        if (!dialog.isSpecial)
        {
            return;
        }
        if (dialog.giftsPokemon)
        {
            foreach (PokemonGiftEntry giftEntry in dialog.giftablePokemon)
            {
                pokemonGiftEntries.Enqueue(giftEntry);
            }
            //gift pokemon in sequential order
        }
        if (dialog.giftsItems)
        {
            //gift items in sequential order
        }

    }

    public void EndDialog()
    {
        Debug.Log("End of conversation");
        GameStateManager.SetState(GameStateManager.GameStates.ROAMING);
        dialogUIManager.HideDialogBox();
    }
}
