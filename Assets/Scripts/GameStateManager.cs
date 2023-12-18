using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameStateManager
{
    public enum GameStates { LOADING, ROAMING, BATTLE, DIALOG};
    public static ObservableClasses.ObservableGameState CurrentGameState = new ObservableClasses.ObservableGameState() { Value = GameStates.LOADING };

    public static GameStates GetState()
    {
        return CurrentGameState.Value;
    }

    public static void SetState(GameStates newGameState) {
        var oldState = CurrentGameState.Value;
        CurrentGameState.SetValue(newGameState);
        Debug.Log("Game State changed to " + Enum.GetName(typeof(GameStates), newGameState).ToLower().FirstCharacterToUpper() + " from " +
            Enum.GetName(typeof(GameStates), oldState).ToLower().FirstCharacterToUpper());
    }
}
