using System;
using System.Collections.Generic;
using System.Linq;

public class PokemonStats
{
    public enum StatTypes { Hp, Attack, Defence, SpecialAttack, SpecialDefence, Speed }
    readonly public Dictionary<StatTypes, int> Stats;

    public PokemonStats(int hp, int attack, int defence, int specialAttack, int specialDefence, int speed)
    {
        Stats = new Dictionary<StatTypes, int>()
        {
            { StatTypes.Hp, hp },
            { StatTypes.Attack, attack }, 
            { StatTypes.Defence, defence},
            { StatTypes.SpecialAttack, specialAttack },
            { StatTypes.SpecialDefence, specialDefence},
            { StatTypes.Speed, speed },
        };
    }

    public PokemonStats(BaseStats baseStats) : this(baseStats.Hp, baseStats.Attack, baseStats.Defence, baseStats.SpecialAttack, baseStats.SpecialDefence, baseStats.Speed) { }
    internal static int CalculateHp(int level, int baseHp, int hpEVs, int hpIVs)
    {
        int hp = (int)(Math.Floor((2 * baseHp + hpIVs + Math.Floor(hpEVs / 4D) * level) / 100) + level + 10);
        return hp;
    }

    internal static int CalculateOtherStat(int level, int baseStat, int EVs, int IVs, Nature nature, Nature.AffectedStats statType)
    {
        double natureModifier = 1.0D;
        if(nature.negativeStat == statType)
        {
            natureModifier = 0.9D;
        } else if(nature.positiveStat == statType)
        {
            natureModifier = 1.1D;
        }
        int stat = (int)Math.Floor((Math.Floor((2 * baseStat + IVs + Math.Floor(EVs / 4D) * level) / 100) + 5) * natureModifier);
        return stat;
    }

    public int CalculateStatTotal()
    {
        return Stats.Values.Sum();
    }

    public int GetStat(StatTypes type)
    {
        return Stats[type];
    }
}