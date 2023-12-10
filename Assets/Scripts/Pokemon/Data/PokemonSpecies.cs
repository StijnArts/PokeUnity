// Ignore Spelling: Pokedex hitbox defence

using Assets.Scripts.Pokemon;
using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Pokemon.Pokedex;
using System;
using System.Collections.Generic;

[Serializable]
public abstract class PokemonSpecies : BaseSpecies
{
    public Dictionary<string, PokemonForm> Forms = new Dictionary<string, PokemonForm>();
    public int NationalPokedexNumber;

    internal PokemonSpecies(
        string pokemonName,
        string pokemonId,
        string primaryType,
        BaseStats baseStats,
        int catchRate,
        double maleRatio,
        int baseExperienceYield,
        string experienceGroup,
        int eggCycles,
        List<string> eggGroups,
        int baseFriendship,
        EvYield evYield,
        double heightInCm,
        double weightInGrams,
        bool hasGenderDifferences = false,
        bool cannotDynamax = false) :
            base(pokemonName, pokemonId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, cannotDynamax, hasGenderDifferences)
    {
    }

    internal PokemonSpecies(
        string pokemonName,
        string pokemonId,
        int nationalPokedexNumber,
        string primaryType,
        BaseStats baseStats,
        int catchRate,
        double maleRatio,
        int baseExperienceYield,
        string experienceGroup,
        int eggCycles,
        List<string> eggGroups,
        int baseFriendship,
        EvYield evYield,
        double heightInCm,
        double weightInGrams,
        bool hasGenderDifferences = false,
        bool cannotDynamax = false) :
            this(pokemonName, pokemonId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, cannotDynamax, hasGenderDifferences)
    {
        NationalPokedexNumber = nationalPokedexNumber;
    }

    public PokemonSpecies(
        string pokemonName,
        string pokemonId,
        string primaryType,
        string secondaryType,
        BaseStats baseStats,
        int catchRate,
        double maleRatio,
        int baseExperienceYield,
        string experienceGroup,
        int eggCycles,
        List<string> eggGroups,
        int baseFriendship,
        EvYield evYield,
        double heightInCm,
        double weightInGrams,
        bool hasGenderDifferences = false,
        bool cannotDynamax = false) :
            this(pokemonName, pokemonId, primaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, cannotDynamax, hasGenderDifferences)
    {
        SecondaryType = secondaryType;
    }

    internal PokemonSpecies(
        string pokemonName,
        string pokemonId,
        int nationalPokedexNumber,
        string primaryType,
        string secondaryType,
        BaseStats baseStats,
        int catchRate,
        double maleRatio,
        int baseExperienceYield,
        string experienceGroup,
        int eggCycles,
        List<string> eggGroups,
        int baseFriendship,
        EvYield evYield,
        double heightInCm,
        double weightInGrams,
        bool hasGenderDifferences = false,
        bool cannotDynamax = false) :
            this(pokemonName, pokemonId, primaryType, secondaryType, baseStats, catchRate, maleRatio, baseExperienceYield, experienceGroup, eggCycles, eggGroups, baseFriendship, evYield, heightInCm, weightInGrams, cannotDynamax, hasGenderDifferences)
    {
        NationalPokedexNumber = nationalPokedexNumber;
    }

    public string classToString()
    {
        string stringOfData = "{" +
            "\npokemonName: " + PokemonName + ",";
        stringOfData += "\nnationalPokedexNumber:" + NationalPokedexNumber + ",";
        stringOfData += "\nprimaryType:" + PrimaryType + ",";
        stringOfData += "\nsecondaryType:" + SecondaryType + ",";
        stringOfData += "\nabilities: [";
        foreach (var ability in Abilities)
        {
            stringOfData += "\n" + ability.AbilityName;
        }
        stringOfData += "\n]," +
            "\nhiddenAbility:" + HiddenAbility.AbilityName + ",";
        stringOfData += "\nbaseStats: {" +
        "\nhp:" + BaseStats.Hp + ",";
        stringOfData += "\nattack:" + BaseStats.Attack + ",";
        stringOfData += "\ndefence:" + BaseStats.Defence + ",";
        stringOfData += "\nspecialAttack:" + BaseStats.SpecialAttack + ",";
        stringOfData += "\nspecialDefence:" + BaseStats.SpecialDefence + ",";
        stringOfData += "\nspeed:" + BaseStats.Speed;
        stringOfData += "\n}," +
            "\ncatchRate:" + CatchRate + ",";
        stringOfData += "\nmaleRatio:" + MaleRatio + ",";
        stringOfData += "\nbaseExperienceYield:" + BaseExperienceYield + ",";
        stringOfData += "\nexperienceGroup:" + ExperienceGroup + ",";
        stringOfData += "\neggCycles:" + EggCycles + ",";
        stringOfData += "\neggGroups: [";
        foreach (string eggGroup in EggGroups)
        {
            stringOfData += "\n" + eggGroup;
        }
        stringOfData += "\n]," +
            "\nmoves: + [";
        //foreach (string move in MoveSets)
        //{
        // stringOfData += "\n" + move;
        //}
        stringOfData += "\n]," +
            "\n]," + 
            "\n]," +
            "\npreEvolution:" + PreEvolution + ",";
        stringOfData += "\nevolutions: [";
        foreach (Evolution evolution in Evolutions)
        {
            stringOfData += evolution.classToString();
        }
        stringOfData += "\n]," +
          "\nbaseFriendship:" + 50 + "," +
          "\nevYield: {" +
          "\n   hp:" + EvYield.Hp + "," +
          "\n   attack:" + EvYield.Attack + "," +
          "\n   defence:" + EvYield.Defence + "," +
          "\n   specialAttack:" + EvYield.SpecialAttack + "," +
          "\n   specialDefence:" + EvYield.SpecialDefence + "," +
          "\n   speed:" + EvYield.Speed + "," +
          "\n}," +
          "\nheight:" + HeightInCm + "," +
          "\nweight:" + WeightInKilos + "," +
          "\ncannotDynamax:" + CannotDynamax +
            "\n}";
        return stringOfData;
    }
}
[Serializable]
public class BaseStats
{
    public BaseStats() { }
    public BaseStats(int hp, int attack, int defence, int specialAttack, int specialDefence, int speed)
    {
        Hp = hp;
        Attack = attack;
        Defence = defence;
        SpecialAttack = specialAttack;
        SpecialDefence = specialDefence;
        Speed = speed;
        BaseStatTotal = hp + attack + defence + specialAttack + specialDefence + speed;
    }
    public int BaseStatTotal;
    public int Hp;
    public int Attack;
    public int Defence;
    public int SpecialAttack;
    public int SpecialDefence;
    public int Speed;
}
[Serializable]
public class Evolution
{
    public Evolution() { }
    public string Id;
    public string Variant;
    public string ResultPokemonId;
    public int ResultFormId;
    public bool ConsumeHeldItem;
    public List<string> LearnableMoves;
    public List<string> Requirements;
    public string RequiredContext;

    public string classToString()
    {
        string classToString = "\n{" +
            "Id:" + this.Id + ",";
        classToString += "Variant:" + this.Variant + ",";
        classToString += "ResultPokemonId:" + this.ResultPokemonId + ",";
        classToString += "ResultFormId:" + this.ResultFormId + ",";
        classToString += "ConsumeHeldItem:" + this.ConsumeHeldItem + ",";
        classToString += "LearnableMoves: [";
        foreach (string move in this.LearnableMoves)
        {
            classToString += "\n" + move + ",";
        }
        classToString += "\n]," +
            "Requirements: [";
        foreach (string requirement in this.Requirements)
        {
            classToString += "\n" + requirement + ",";
        }
        classToString += "RequiredContext:" + this.RequiredContext +
            "\n},";
        return classToString;
    }
}
[Serializable]
public class EvYield
{
    public EvYield() { }
    public int Hp;
    public int Attack;
    public int Defence;
    public int SpecialAttack;
    public int SpecialDefence;
    public int Speed;
}
[Serializable]
public class Hitbox
{
    public Hitbox() { }
    public double Width;
    public double Height;
}

