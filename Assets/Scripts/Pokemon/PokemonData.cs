// Ignore Spelling: Pokedex hitbox defence

using System;
using System.Collections.Generic;
using UnityEditor.Playables;

[Serializable]
public class PokemonData
{
    public Dictionary<string, PokemonData> Forms = new Dictionary<string, PokemonData>();
    public string PokemonName;
    public string PokemonId;
    public int NationalPokedexNumber;
    public string PrimaryType;
    public string SecondaryType;
    public List<Ability.Abilities> Abilities;
    public Ability.Abilities HiddenAbility;
    public BaseStats BaseStats;
    public int CatchRate;
    public double MaleRatio;
    public int BaseExperienceYield;
    public string ExperienceGroup;
    public int EggCycles;
    public List<string> EggGroups;
    public List<string> Moves;
    public List<Pokedex> Pokedex;
    public string PreEvolution;
    public List<Evolution> Evolutions;
    public Hitbox Hitbox;
    public int BaseFriendship;
    public EvYield EvYield;
    public double Height;
    public double Weight;
    public bool CannotDynamax;
    public bool HasGenderDifferences;
    public string classToString()
    {
        string stringOfData = "{" +
            "\npokemonName: " + PokemonName + ",";
        stringOfData += "\nnationalPokedexNumber:" + NationalPokedexNumber + ",";
        stringOfData += "\nprimaryType:" + PrimaryType + ",";
        stringOfData += "\nsecondaryType:" + SecondaryType + ",";
        stringOfData += "\nabilities: [";
        foreach (Ability.Abilities ability in Abilities)
        {
            stringOfData += "\n" + AbilityRegistry.GetAbility(ability).AbilityName;
        }
        stringOfData += "\n]," +
            "\nhiddenAbility:" + AbilityRegistry.GetAbility(HiddenAbility).AbilityName + ",";
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
        foreach (string move in Moves)
        {
            stringOfData += "\n" + move;
        }
        stringOfData += "\n]," +
            "\n]," +
            "\npokedex: [";
        foreach (Pokedex pokedex in Pokedex)
        {
            stringOfData +=
            "\n{" +
                "Version:" + pokedex.Version +
                "Text:" + pokedex.Text +
            "\n},";
        }
        stringOfData += "\n]," +
            "\npreEvolution:" + PreEvolution + ",";
        stringOfData += "\nevolutions: [";
        foreach (Evolution evolution in Evolutions)
        {
            stringOfData += evolution.classToString();
        }
        stringOfData += "\n],";
        stringOfData += "\nhitbox: {" +
                "\nwidth:" + Hitbox.Width + ",";
        stringOfData += "\nheight:" + Hitbox.Height + ",";
        stringOfData += "\n}," +
                 "\nbaseFriendship:" + 50 + ",";
        stringOfData += "\nevYield: {" +
                "\nhp:" + EvYield.Hp + ",";
        stringOfData += "\nattack:" + EvYield.Attack + ",";
        stringOfData += "\ndefence:" + EvYield.Defence + ",";
        stringOfData += "\nspecialAttack:" + EvYield.SpecialAttack + ",";
        stringOfData += "\nspecialDefence:" + EvYield.SpecialDefence + ",";
        stringOfData += "\nspeed:" + EvYield.Speed + ",";
        stringOfData += "\n}," +
            "\nheight:" + Height + ",";
        stringOfData += "\nweight:" + Weight + ",";
        stringOfData += "\ncannotDynamax:" + CannotDynamax +
            "\n}";
        return stringOfData;
    }
}
[Serializable]
public class BaseStats
{
    public BaseStats() { }
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
[Serializable]
public class Pokedex
{
    public Pokedex() { }
    public string Version;
    public string Text;
}

