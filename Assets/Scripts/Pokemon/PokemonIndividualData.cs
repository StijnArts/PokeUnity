using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PokemonIndividualData
{
    public List<Ability> possibleAbilities = AbilityRegistry.Abilities.Values.ToList();
    [HideInInspector]
    public int nationalPokedexNumber;
    public string pokemonName;
    public string pokemonId;
    public ObservableClasses.ObservableInteger level = new ObservableClasses.ObservableInteger() { Value = 1 };//TODO make move learning and evolutions subscribe to the level observable
    public string nickname = null;
    [HideInInspector]
    public int currentHp;
    public string formId = "";
    public Move[] moves = new Move[4];
    public string Ability;
    [HideInInspector]
    public Ability abilityData;
    public Nature.Natures nature = Nature.Natures.Adamant;
    [HideInInspector]
    public Nature natureData;
    public PokemonType primaryType;
    public PokemonType secondaryType = null;
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

    public PokemonStats calculateStats(int level, PokemonEVs pokemonEVs, PokemonIVs pokemonIVs, Nature nature)
    {
        BaseStats baseStats = PokemonRegistry.GetPokemonSpecies(pokemonId).BaseStats;
        int hp = PokemonStats.calculateHp(level, baseStats.Hp, pokemonEVs.hpEVs, pokemonIVs.hpIVs);
        int attack = PokemonStats.calculateOtherStat(level, baseStats.Attack, pokemonEVs.attackEVs,
            pokemonIVs.attackIVs, nature, Nature.AffectedStats.ATTACK);
        int defence = PokemonStats.calculateOtherStat(level, baseStats.Defence, pokemonEVs.defenceEVs,
            pokemonIVs.defenceIVs, nature, Nature.AffectedStats.DEFENCE);
        int specialAttack = PokemonStats.calculateOtherStat(level, baseStats.SpecialAttack, pokemonEVs.specialAttackEVs,
            pokemonIVs.specialAttackIVs, nature, Nature.AffectedStats.SPECIAL_ATTACK);
        int specialdefence = PokemonStats.calculateOtherStat(level, baseStats.SpecialDefence, pokemonEVs.specialdefenceEVs,
            pokemonIVs.specialdefenceIVs, nature, Nature.AffectedStats.SPECIAL_DEFENCE);
        int speed = PokemonStats.calculateOtherStat(level, baseStats.Speed, pokemonEVs.speedEVs,
            pokemonIVs.speedIVs, nature, Nature.AffectedStats.SPEED);

        return new PokemonStats(hp, attack, defence, specialAttack, specialdefence, speed);
    }

    public PokemonStats calculateStats()
    {
        BaseStats baseStats = PokemonRegistry.GetPokemonSpecies(pokemonId).BaseStats;
        int hp = PokemonStats.calculateHp(level.Value, baseStats.Hp, EVs.hpEVs, IVs.hpIVs);
        int attack = PokemonStats.calculateOtherStat(level.Value, baseStats.Attack, EVs.attackEVs,
            IVs.attackIVs, natureData, Nature.AffectedStats.ATTACK);
        int defence = PokemonStats.calculateOtherStat(level.Value, baseStats.Defence, EVs.defenceEVs,
            IVs.defenceIVs, natureData, Nature.AffectedStats.DEFENCE);
        int specialAttack = PokemonStats.calculateOtherStat(level.Value, baseStats.SpecialAttack, EVs.specialAttackEVs,
            IVs.specialAttackIVs, natureData, Nature.AffectedStats.SPECIAL_ATTACK);
        int specialdefence = PokemonStats.calculateOtherStat(level.Value, baseStats.SpecialDefence, EVs.specialdefenceEVs,
            IVs.specialdefenceIVs, natureData, Nature.AffectedStats.SPECIAL_DEFENCE);
        int speed = PokemonStats.calculateOtherStat(level.Value, baseStats.Speed, EVs.speedEVs,
            IVs.speedIVs, natureData, Nature.AffectedStats.SPEED);

        return new PokemonStats(hp, attack, defence, specialAttack, specialdefence, speed);
    }
}
