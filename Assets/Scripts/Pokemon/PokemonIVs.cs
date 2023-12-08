using System;
using UnityEngine;

[Serializable]
public class PokemonIVs
{
    [Range(0, 31)]
    public int hpIVs;
    [Range(0, 31)]
    public int attackIVs;
    [Range(0, 31)]
    public int defenceIVs;
    [Range(0, 31)]
    public int specialAttackIVs;
    [Range(0, 31)]
    public int specialdefenceIVs;
    [Range(0, 31)]
    public int speedIVs;


    public PokemonIVs(int hpIVs, int attackIVs, int defenceIVs, int specialAttackIVs, int specialdefenceIVs, int speedIVs)
    {
        this.hpIVs = hpIVs;
        this.attackIVs = attackIVs;
        this.defenceIVs = defenceIVs;
        this.specialdefenceIVs = specialdefenceIVs;
        this.specialAttackIVs = specialAttackIVs;
        this.speedIVs = speedIVs;
    }

    public PokemonIVs()
    {
        this.hpIVs = 0;
        this.attackIVs = 0;
        this.defenceIVs = 0;
        this.specialdefenceIVs = 0;
        this.specialAttackIVs = 0;
        this.speedIVs = 0;
    }

    public int calculateRemainingIVs()
    {
        //TODO calculate the remaining IVs based off of a static variable that functions as a setting.
        return 0;
    }
}