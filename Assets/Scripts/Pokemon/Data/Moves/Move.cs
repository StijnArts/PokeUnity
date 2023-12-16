using Assets.Scripts.Battle;
using Assets.Scripts.Battle.Conditions;
using Assets.Scripts.Pokemon.Data.Conditions;
using Assets.Scripts.Pokemon.Data.Moves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Battle.ActiveMove;

public abstract class Move
{
    public enum SelfDestructStates
    {
        Always, IfHit, True, False
    };
    public string MoveId, MoveName, MoveType, MoveCategory;
    public int PP, BasePower, Priority;
    public Target.TargettingType MoveTarget;
    public string IsZ = null;
    public bool? IsMax = null;
    public string MaxMoveRecipient = null;
    public ConditionData Condition;
    public int? Accuracy;
    public List<string> MoveFlags;
    public string RealMove;
    public int? Damage;
    public bool? DamageBasedOnLevel;
    public string ContestType;
    public bool? NoPPBoosts;
    public ZMove Zmove;
    public int? MaxMoveBasePower;
    public bool? OneHitKO;
    public string OneHitKOType;
    public bool? ThawsTarget;
    public int[] Heal;
    public bool? ForceSwitch;
    public SelfSwitchStates? SelfSwitch;
    public Dictionary<PokemonStats.StatTypes, int> SelfBoost;
    public SelfDestructStates? SelfDestruct;
    public bool? BreaksProtect;
    public int[] Recoil;
    public int[] Drain;
    public bool? MindBlownRecoil;
    public bool? StealsBoosts;
    public bool? StruggleRecoil;
    public SecondaryEffect Secondary;
    public List<SecondaryEffect> Secondaries;
    public SecondaryEffect Self;
    public bool? HasSheerForce;
    public string BaseMoveType;


    public Move(string moveId, string moveName, string moveType, string category, int pP, Target.TargettingType targettingType, int accuracy, string contestType)
    {
        MoveId = moveId;
        MoveName = moveName;
        MoveType = moveType;
        PP = pP;
        MoveCategory = category;
        MoveTarget = targettingType;
        Accuracy = accuracy;
        ContestType = contestType;
    }
}
