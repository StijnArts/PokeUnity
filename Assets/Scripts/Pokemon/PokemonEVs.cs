
using System;
using UnityEngine;

[Serializable]
public class PokemonEVs
{
    [Range(0, 255)]
    public int hpEVs;
    [Range(0, 255)]
    public int attackEVs;
    [Range(0, 255)]
    public int defenceEVs;
    [Range(0, 255)]
    public int specialAttackEVs;
    [Range(0, 255)]
    public int specialdefenceEVs;
    [Range(0, 255)]
    public int speedEVs;


    public PokemonEVs(int hpEVs, int attackEVs, int defenceEVs, int specialAttackEVs, int specialdefenceEVs, int speedEVs)
    {
        this.hpEVs = hpEVs;
        this.attackEVs = attackEVs;
        this.defenceEVs = defenceEVs;
        this.specialdefenceEVs = specialdefenceEVs;
        this.specialAttackEVs = specialAttackEVs;
        this.speedEVs = speedEVs;
    }

    public PokemonEVs()
    {
        this.hpEVs = 0;
        this.attackEVs = 0;
        this.defenceEVs = 0;
        this.specialdefenceEVs = 0;
        this.specialAttackEVs = 0;
        this.speedEVs = 0;
    }

    public int calculateRemainingEVs()
    {
        //TODO calculate the remaining evs based off of a static variable that functions as a setting.
        return 0;
    }
}