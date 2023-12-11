using Assets.Scripts.Pokemon.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PokemonIndividualData
{
    [HideInInspector]
    public string PokemonName;
    public string PokemonId;
    public ObservableClasses.ObservableInteger level = new ObservableClasses.ObservableInteger() { Value = 1 };//TODO make move learning and evolutions subscribe to the level observable
    public string nickname = null;
    [HideInInspector]
    public int CurrentHp;
    public string FormId = "";
    public Move[] Moves = new Move[4];
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
    public Pokemon.PokemonGender gender = Pokemon.PokemonGender.MALE;
    [HideInInspector]
    public PokemonStats stats = new PokemonStats(1, 1, 1, 1, 1, 1);
    [HideInInspector]
    public PokemonStats baseStats = new PokemonStats(1, 1, 1, 1, 1, 1);
    public PokemonEVs EVs = new PokemonEVs();
    public PokemonIVs IVs = new PokemonIVs();
    public PokemonItem heldItem = null;
    [HideInInspector]
    public bool isValid = false;
    public int currentExperience = 0;
    public int friendship = 0;
    public List<Move> learnableMoves = new List<Move>();
    public bool isSavedPokemon = false;

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
        int hp = PokemonStats.CalculateHp(level.Value, baseStats.Hp, EVs.hpEVs, IVs.hpIVs);
        int attack = PokemonStats.CalculateOtherStat(level.Value, baseStats.Attack, EVs.attackEVs,
            IVs.attackIVs, NatureData, global::Nature.AffectedStats.ATTACK);
        int defence = PokemonStats.CalculateOtherStat(level.Value, baseStats.Defence, EVs.defenceEVs,
            IVs.defenceIVs, NatureData, global::Nature.AffectedStats.DEFENCE);
        int specialAttack = PokemonStats.CalculateOtherStat(level.Value, baseStats.SpecialAttack, EVs.specialAttackEVs,
            IVs.specialAttackIVs, NatureData, global::Nature.AffectedStats.SPECIAL_ATTACK);
        int specialdefence = PokemonStats.CalculateOtherStat(level.Value, baseStats.SpecialDefence, EVs.specialdefenceEVs,
            IVs.specialdefenceIVs, NatureData, global::Nature.AffectedStats.SPECIAL_DEFENCE);
        int speed = PokemonStats.CalculateOtherStat(level.Value, baseStats.Speed, EVs.speedEVs,
            IVs.speedIVs, NatureData, global::Nature.AffectedStats.SPEED);

        return new PokemonStats(hp, attack, defence, specialAttack, specialdefence, speed);
    }

    internal void Initialize()
    {
        NatureData = PokemonNatureRegistry.GetNature((int)Nature);

        var species = PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId));
        Ability = AbilityRegistry.GetAbility(AbilityData);
        PrimaryType = PokemonTypeRegistry.GetType(species.PrimaryType);
        stats = CalculateStats();
        if (species.SecondaryType != null)
        {
            SecondaryType = PokemonTypeRegistry.GetType(species.SecondaryType);
        }

        if (!string.IsNullOrEmpty(FormId) && species.Forms.ContainsKey(FormId))
        {
            //TODO overwrite species data where form data is not null
        }
    }
}
