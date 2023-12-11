using System;

public class PokemonStats
{
    public int hp;
    public int attack;
    public int defence;
    public int specialAttack;
    public int specialdefence;
    public int speed;

    public PokemonStats(int hp, int attack, int defence, int specialAttack, int specialdefence, int speed)
    {
        this.hp = hp;
        this.attack = attack;
        this.defence = defence;
        this.specialAttack = specialAttack;
        this.specialdefence = specialdefence;
        this.speed = speed;
    }

    public PokemonStats(BaseStats baseStats)
    {
        this.hp = baseStats.Hp;
        this.attack = baseStats.Attack;
        this.defence = baseStats.Defence;
        this.specialAttack = baseStats.SpecialAttack;
        this.specialdefence = baseStats.SpecialDefence;
        this.speed = baseStats.Speed;
    }

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

    public int calculateStatTotal()
    {
        return (hp + attack + defence + specialAttack + specialdefence + speed);
    }
}