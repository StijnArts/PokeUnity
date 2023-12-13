using Assets.Scripts.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move
{
    public string MoveId, MoveName, MoveType, MoveCategory;
    public int PowerPoints, BasePower;
    public Target.TargettingType TargettingType;
    public Move(string moveId, string moveName, string moveType, string category, int powerPoints, Target.TargettingType targettingType)
    {
        MoveId = moveId;
        MoveName = moveName;
        MoveType = moveType;
        PowerPoints = powerPoints;
        MoveCategory = category;
        TargettingType = targettingType;
    }
}
