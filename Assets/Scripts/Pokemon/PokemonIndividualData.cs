using Assets.Scripts.Battle;
using Assets.Scripts.Pokemon;
using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Pokemon.Data.Moves;
using Assets.Scripts.Pokemon.PokemonTypes;
using Assets.Scripts.Registries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class PokemonIndividualData
{
    public string PokemonId;
    public string PokemonName;
    [HideInInspector]
    public PokemonSpecies BaseSpecies;
    public ObservableClasses.ObservableInteger Level = new ObservableClasses.ObservableInteger() { Value = 1 };//TODO make move learning and evolutions subscribe to the level observable
    public string Nickname = null;
    [HideInInspector]
    public int CurrentHp;
    public string FormId = "";
    public MoveSlot[] Moves = new MoveSlot[Settings.MaxMoveSlots];
    public string Ability;
    [HideInInspector]
    public Nature.Natures Nature = global::Nature.Natures.Adamant;
    [HideInInspector]
    public Nature NatureData;
    [HideInInspector]
    public float HitboxHeight;
    [HideInInspector]
    public float HitboxWidth;
    public bool IsShiny = false;
    public PokemonNpc.PokemonGender gender = PokemonNpc.PokemonGender.MALE;
    [HideInInspector]
    public PokemonStats Stats = new PokemonStats(1, 1, 1, 1, 1, 1);
    [HideInInspector]
    public PokemonStats BaseStats = new PokemonStats(1, 1, 1, 1, 1, 1);
    public PokemonEVs EVs = new PokemonEVs();
    public PokemonIVs IVs = new PokemonIVs();
    public string Item;
    [HideInInspector]
    public bool IsValid = false;
    public int CurrentExperience = 0;
    public int Friendship = 0;
    public List<Move> LearnableMoves = new List<Move>();
    public bool IsSavedPokemon = false;
    [HideInInspector]
    public PokemonBattleData BattleData;
    public string Status;
    public bool Fainted => CurrentHp >= 0;

    public string TeraType;

    public string PokeBall;

    //TODO check hiddenpower values when initializing
    public string HiddenPowerType;
    public int HiddenPowerPower;

    public string GetSpriteSuffix()
    {
        string suffix = "";
        if (pokemonHasForm(BaseSpecies, FormId))
        {
            suffix += "_" + FormId;
        }
        if (pokemonOrFormHasGenderDifferences(BaseSpecies, FormId) && gender == PokemonNpc.PokemonGender.FEMALE)
        {
            suffix += "_female";
        }
        if (IsShiny)
        {
            suffix += "_shiny";
        }
        return suffix;
    }

    private bool pokemonOrFormHasGenderDifferences(PokemonSpecies pokemonSpecies, string formId)
    {
        if (pokemonHasForm(pokemonSpecies, formId))
        {
            return pokemonSpecies.Forms[formId].HasGenderDifferences;
        }
        else
        {
            return pokemonSpecies.HasGenderDifferences;
        }
    }

    private static bool pokemonHasForm(PokemonSpecies pokemonSpecies, string formId)
    {
        return pokemonSpecies.Forms.ContainsKey(formId);
    }

    private PokemonStats CalculateStats(int level, PokemonEVs pokemonEVs, PokemonIVs pokemonIVs, Nature nature)
    {
        BaseStats baseStats = BaseSpecies.BaseStats;

        int hp = PokemonStats.CalculateHp(level, baseStats.Hp, pokemonEVs.hpEVs, pokemonIVs.hpIVs);
        int attack = PokemonStats.CalculateOtherStat(level, baseStats.Attack, pokemonEVs.attackEVs,
            pokemonIVs.attackIVs, nature, global::Nature.AffectedStats.ATTACK);
        int defence = PokemonStats.CalculateOtherStat(level, baseStats.Defence, pokemonEVs.defenceEVs,
            pokemonIVs.defenceIVs, nature, global::Nature.AffectedStats.DEFENCE);
        int specialAttack = PokemonStats.CalculateOtherStat(level, baseStats.SpecialAttack, pokemonEVs.specialAttackEVs,
            pokemonIVs.specialAttackIVs, nature, global::Nature.AffectedStats.SPECIAL_ATTACK);
        int specialdefence = PokemonStats.CalculateOtherStat(level, baseStats.SpecialDefence, pokemonEVs.specialdefenceEVs,
            pokemonIVs.specialdefenceIVs, nature, global::Nature.AffectedStats.SPECIAL_DEFENCE);
        int speed = PokemonStats.CalculateOtherStat(level, baseStats.Speed, pokemonEVs.speedEVs,
            pokemonIVs.speedIVs, nature, global::Nature.AffectedStats.SPEED);

        return new PokemonStats(hp, attack, defence, specialAttack, specialdefence, speed);
    }

    private PokemonStats CalculateStats()
    {
        BaseStats baseStats = BaseSpecies.BaseStats;
        int hp = PokemonStats.CalculateHp(Level.Value, baseStats.Hp, EVs.hpEVs, IVs.hpIVs);
        int attack = PokemonStats.CalculateOtherStat(Level.Value, baseStats.Attack, EVs.attackEVs,
            IVs.attackIVs, NatureData, global::Nature.AffectedStats.ATTACK);
        int defence = PokemonStats.CalculateOtherStat(Level.Value, baseStats.Defence, EVs.defenceEVs,
            IVs.defenceIVs, NatureData, global::Nature.AffectedStats.DEFENCE);
        int specialAttack = PokemonStats.CalculateOtherStat(Level.Value, baseStats.SpecialAttack, EVs.specialAttackEVs,
            IVs.specialAttackIVs, NatureData, global::Nature.AffectedStats.SPECIAL_ATTACK);
        int specialdefence = PokemonStats.CalculateOtherStat(Level.Value, baseStats.SpecialDefence, EVs.specialdefenceEVs,
            IVs.specialdefenceIVs, NatureData, global::Nature.AffectedStats.SPECIAL_DEFENCE);
        int speed = PokemonStats.CalculateOtherStat(Level.Value, baseStats.Speed, EVs.speedEVs,
            IVs.speedIVs, NatureData, global::Nature.AffectedStats.SPEED);

        return new PokemonStats(hp, attack, defence, specialAttack, specialdefence, speed);
    }

    internal void Initialize()
    {
        NatureData = PokemonNatureRegistry.GetNature((int)Nature);

        BaseSpecies = PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId));
        
        Stats = CalculateStats();

        if (!string.IsNullOrEmpty(FormId) && BaseSpecies.Forms.ContainsKey(FormId))
        {
            //TODO overwrite species data where form data is not null
        }
    }

    public int GetSpriteWidth()
    {
        return PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId)).SpriteWidth;
    }

    public int GetSpriteResolution()
    {
        return PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId)).SpriteResolution;
    }

    public int GetSpriteAnimationSpeed()
    {
        return PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId)).SpriteAnimationSpeed;
    }

    public string GetName()
    {
        return string.IsNullOrEmpty(Nickname) ? PokemonName : Nickname;
    }
}
