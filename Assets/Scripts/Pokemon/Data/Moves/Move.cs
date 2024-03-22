using Assets.Scripts.Battle;
using System;
using System.Collections.Generic;

[Serializable]
public abstract class Move
{
    public enum SelfDestructStates
    {
        Always, IfHit, True, False
    };

    public string Id;
    public string Name;
    public string MoveType;
    public Target.TargettingType MoveTarget;
    public int BasePower;
    public int Accuracy;
    public double? CritRatio;
    public string BaseMoveType;
    public bool HasSheerForce = false;
    public int Priority = 0;
    public string MoveCategory;
    public PokemonStats.StatTypes? OverrideOffensiveStat;
    public OverridePokemonOptions? OverrideOffensivePokemon;
    public PokemonStats.StatTypes? OverrideDefensiveStat;
    public OverridePokemonOptions? OverrideDefensivePokemon;
    public bool IgnoreDefensive = false;
    public bool IgnoreImmunity = false;
    public bool IgnoreNegativeOffense = false;
    public bool IgnoreOffensive = false;
    public bool IgnorePositiveDefensive = false;
    public int PP;
    public bool NoPPBoosts = false;
    public string IsZ = null;
    public bool IsMax = false;
    public List<string> MoveFlags = new();
    public string MaxMoveRecipient = null;
    public Target.TargettingType? PressureTarge;
    public Target.TargettingType? NonGhostTarget;
    public bool IgnoreAbility = false;
    public int? Damage;
    public bool DamageBasedOnLevel = false;
    public bool SpreadHit = false;
    public bool ForceStab = false;
    public bool NoSketch = false;
    public double Stab = 1.5;
    public string VolatileStatus;

    public string RealMove;
    public string ContestType;
    public int? MaxMoveBasePower;
    public bool? OneHitKO;
    public string OneHitKOType;
    public bool? ThawsTarget;
    public int[] Heal;
    public bool? ForceSwitch;
    public Dictionary<PokemonStats.StatTypes, int> SelfBoost;
    public Dictionary<PokemonStats.StatTypes, int> BoostsOnTarget;
    public SelfDestructStates? SelfDestruct;
    public bool? BreaksProtect;
    public int[] Recoil;
    public int[] Drain;
    public bool? MindBlownRecoil;
    public bool? StealsBoosts;
    public bool? StruggleRecoil;
    public double? BasePowerModifer;
    public double? CritModifier;
    public enum OverridePokemonOptions { Target, Source}
    public bool? MultiAccuracy;
    public int[] MultiHit;
    public string MultiHitType;
    public bool? NoDamageVariance;
    public double? SpreadModifier;
    public bool? SleepUsable;
    public bool? SmartTarget;
    public bool? TracksTarget;
    public bool? WillCrit;


    public bool? HasCrashDamage;
    public bool? IsConfusionSelfHit;
    public string[] NoMetronome;
    public bool? StallingMove;
    public string BaseMove;
    public bool IgnorePositiveEvasion = false;
    public bool IgnoreEvasion = false;
    public bool IgnoreAccuracy = false;


    public Move(string moveId, string moveName, string moveType, string category, int pP, Target.TargettingType targettingType, int accuracy)
    {
        Id = moveId;
        Name = moveName;
        MoveType = moveType;
        PP = pP;
        MoveCategory = category;
        MoveTarget = targettingType;
        Accuracy = accuracy;

        //Initializing Optional Fields
        BaseMoveType = moveType;
    }

    public Dictionary<PokemonStats.StatTypes, int> GetBoosts() => BoostsOnTarget;
    public string GetStatus() => null;
    public string GetVolatileStatus() => null;
    public string GetSideCondition() => null;
    public string GetSlotCondition() => null;
    public string GetPseudoWeather() => null;
    public string GetTerrain() => null;
    public string GetWeather() => null;
}
