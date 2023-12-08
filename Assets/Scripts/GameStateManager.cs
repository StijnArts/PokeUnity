using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{
    public enum GameStates { LOADING, ROAMING, BATTLE, DIALOG};
    public static ObservableClasses.ObservableGameState currentGameState = new ObservableClasses.ObservableGameState() { Value = GameStates.LOADING };

    public static GameStates GetState()
    {
        return currentGameState.Value;
    }

    public static void SetState(GameStates newGameState) {
        currentGameState.SetValue(newGameState);
    }
}
