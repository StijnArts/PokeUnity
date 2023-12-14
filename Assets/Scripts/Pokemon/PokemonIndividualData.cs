using Assets.Scripts.Battle;
using Assets.Scripts.Battle.Conditions;
using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using Assets.Scripts.Battle.Events.Sources;
using Assets.Scripts.Pokemon;
using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PokemonIndividualData : Target, BattleEventSource, EffectHolder
{
    [HideInInspector]
    public string PokemonName;
    public string PokemonId;
    public ObservableClasses.ObservableInteger Level = new ObservableClasses.ObservableInteger() { Value = 1 };//TODO make move learning and evolutions subscribe to the level observable
    public string Nickname = null;
    [HideInInspector]
    public int? CurrentHp;
    public string FormId = "";
    public PokemonMove[] Moves = new PokemonMove[Settings.MaxMoveSlots];
    public string AbilityData;
    [HideInInspector]
    public Ability Ability;
    public Nature.Natures Nature = global::Nature.Natures.Adamant;
    [HideInInspector]
    public Nature NatureData;
    public PokemonType PrimaryType;
    public PokemonType SecondaryType = null;
    [HideInInspector]
    public float hitboxHeight;
    [HideInInspector]
    public float hitboxWidth;
    public bool isShiny = false;
    public PokemonNpc.PokemonGender gender = PokemonNpc.PokemonGender.MALE;
    [HideInInspector]
    public PokemonStats Stats = new PokemonStats(1, 1, 1, 1, 1, 1);
    [HideInInspector]
    public PokemonStats BaseStats = new PokemonStats(1, 1, 1, 1, 1, 1);
    public PokemonEVs EVs = new PokemonEVs();
    public PokemonIVs IVs = new PokemonIVs();
    public PokemonItem heldItem = null;
    [HideInInspector]
    public bool isValid = false;
    public int currentExperience = 0;
    public int friendship = 0;
    public List<Move> learnableMoves = new List<Move>();
    public bool isSavedPokemon = false;
    [HideInInspector]
    public PokemonBattleData BattleData;
    public string Status;
    public EffectState StatusState;
    public Dictionary<string, EffectState> Volatiles;
    public string GetSpriteSuffix()
    {
        string suffix = "";
        if (pokemonHasForm(PokemonId, FormId))
        {
            suffix += "_" + FormId;
        }
        if (pokemonOrFormHasGenderDifferences(PokemonId, FormId) && gender == PokemonNpc.PokemonGender.FEMALE)
        {
            suffix += "_female";
        }
        if (isShiny)
        {
            suffix += "_shiny";
        }
        return suffix;
    }

    private bool pokemonOrFormHasGenderDifferences(string pokemonId, string formId)
    {
        if (pokemonHasForm(pokemonId, formId))
        {
            return PokemonRegistry.GetPokemonSpecies(pokemonId).Forms[formId].HasGenderDifferences;
        }
        else
        {
            return PokemonRegistry.GetPokemonSpecies(pokemonId).HasGenderDifferences;
        }
    }

    private static bool pokemonHasForm(string pokemonId, string formId)
    {
        return PokemonRegistry.GetPokemonSpecies(pokemonId).Forms.ContainsKey(formId);
    }

    private PokemonStats CalculateStats(int level, PokemonEVs pokemonEVs, PokemonIVs pokemonIVs, Nature nature)
    {
        BaseStats baseStats = PokemonRegistry.GetPokemonSpecies(PokemonId).BaseStats;

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
        BaseStats baseStats = PokemonRegistry.GetPokemonSpecies(PokemonId).BaseStats;
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

        var species = PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId));
        Ability = AbilityRegistry.GetAbility(AbilityData);
        PrimaryType = PokemonTypeRegistry.GetType(species.PrimaryType);
        Stats = CalculateStats();
        if (species.SecondaryType != null)
        {
            SecondaryType = PokemonTypeRegistry.GetType(species.SecondaryType);
        }

        if (!string.IsNullOrEmpty(FormId) && species.Forms.ContainsKey(FormId))
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

    public void SetBattleData(PokemonBattleData pokemonBattleData)
    {
        BattleData = pokemonBattleData;
    }

    public List<PokemonIndividualData> GetAllyAndSelf()
    {
        return BattleData.BattleController.ActivePokemon.Select(pokemon => pokemon.PokemonIndividualData).ToList();
    }

    public List<PokemonIndividualData> GetFoes(Dictionary<BattleController, List<PokemonNpc>> activePokemon)
    {
        return activePokemon.Where(pair=>pair.Key != BattleData.BattleController).SelectMany(pair => pair.Value).Select(pokemon => pokemon.PokemonIndividualData).ToList();
    }

    public Condition GetStatus()
    {
        return StatusRegistry.GetStatusById(Status);
    }

    public bool ClearStatus()
    {
        if(CurrentHp == null || Status == null) return false;
        if (Status == "slp" && RemoveVolatile("nightmare")){
            //TODO send message in battle dialog;
        }
        Status = "";
        return true;
    }

    public bool RemoveVolatile(string volatileStatusId)
    {
        if(CurrentHp == null) return false;
        var statusEffect = StatusRegistry.GetStatusById(volatileStatusId);
        if (statusEffect == null) return false;
        var volatileStatus = Volatiles[statusEffect.Id];
        BattleData.Battle.SingleEvent("End", statusEffect, volatileStatus, this);
        var linkedPokemon = volatileStatus["LinkedPokemon"];
        var linkedStatus = volatileStatus["LinkedStatus"];
        Volatiles.Remove(statusEffect.Id);
        if( linkedPokemon != null)
        {
            this.RemoveLinkedVolatiles(linkedStatus, (List<PokemonIndividualData>)linkedPokemon);
        }
        return true;
    }

    public void RemoveLinkedVolatiles(object linkedStatus, List<PokemonIndividualData> linkedPokemon)
    {
        var linkedStatusResolver = (linkedStatus != null 
            ? (linkedStatus is Effect ? linkedStatus.ToString() : 
              (linkedStatus is string) ? linkedStatus : throw new ArgumentException("Linkedstatus must be of type string or Effect"))
                                                      : throw new ArgumentNullException("linkedStatus cannot Be null"));
        var linkedStatusResolved = linkedStatusResolver as string;
        foreach(var linkedPoke in linkedPokemon)
        {
            var volatileData = linkedPoke.Volatiles[linkedStatusResolved];
            if(volatileData == null) continue;
            var volatileDataLinkedPokemon = (List<PokemonIndividualData>)volatileData["LinkedPokemon"];
            volatileDataLinkedPokemon.Remove(this);
            volatileData["LinkedPokemon"] = volatileDataLinkedPokemon;
            if (volatileDataLinkedPokemon.Count == 0)
            {
                linkedPoke.RemoveVolatile(linkedStatusResolved);
            }
        }
    }

    public int GetStat(PokemonStats.StatTypes statType, bool unboosted, bool unmodified) => BattleData.GetStat(this, statType, unboosted, unmodified);
}
