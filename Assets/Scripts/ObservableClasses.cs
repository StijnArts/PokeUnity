using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObservables;

public class ObservableClasses : MonoBehaviour
{
    [Serializable]
    public class ObservableInteger : Observable<int> { }
    public class ObservableVector3 : Observable<Vector3> { }
    public class ObservableFloat : Observable<float> { }
    public class ObservableBoolean : Observable<bool> { }
    public class ObservableString : Observable<string> { }
    public class ObservableGameState : Observable<GameStateManager.GameStates> { }
}
