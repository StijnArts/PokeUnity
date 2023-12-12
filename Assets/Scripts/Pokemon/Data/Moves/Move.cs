using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move
{
    public string MoveId, MoveName, MoveType, MoveCategory;
    public int PowerPoints, BasePower;
    public Move(string moveId, string moveName, string moveType, string category, int powerPoints)
    {
        MoveId = moveId;
        MoveName = moveName;
        MoveType = moveType;
        PowerPoints = powerPoints;
        MoveCategory = category;
    }
}
