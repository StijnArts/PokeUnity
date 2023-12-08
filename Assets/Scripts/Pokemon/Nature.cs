
using UnityEngine;
using System;

[Serializable]
public class Nature
{
    public enum Natures:int {
        Adamant = 0, 
        Bashful=1,  
        Brave = 2,
        Bold = 3,
        Calm = 4,
        Careful = 5,
        Docile = 6,
        Gentle = 7,
        Hardy = 8,
        Hasty = 9,
        Impish = 10,
        Jolly = 11,
        Lax= 12,
        Lonely= 13,
        Mild = 14,
        Modest = 15,
        Naive = 16,
        Naughty = 17,
        Quiet = 18,
        Quirky = 19,
        Rash = 20,
        Relaxed = 21,
        Sassy = 22,
        Serious = 23,
        Timid = 24
    }
    public enum AffectedStats { ATTACK, DEFENCE, SPECIAL_ATTACK, SPECIAL_DEFENCE, SPEED, NEUTRAL };
    public enum Flavor { SPICY, SOUR, SWEET, DRY, BITTER, NEUTRAL};
    public AffectedStats positiveStat = AffectedStats.NEUTRAL;
    public AffectedStats negativeStat = AffectedStats.NEUTRAL;
    [HideInInspector]
    public Flavor favoriteFlavor = Flavor.NEUTRAL;
    [HideInInspector]
    public Flavor dislikedFlavor = Flavor.NEUTRAL;
    [HideInInspector]
    public string natureName;

    public Nature(string natureName, AffectedStats positiveStat, AffectedStats negativeStat, Flavor favoriteFlavor, Flavor dislikedFlavor)
    {
        this.positiveStat = positiveStat;
        this.negativeStat = negativeStat;
        this.favoriteFlavor = favoriteFlavor;
        this.dislikedFlavor = dislikedFlavor;
        this.natureName = natureName;
    }

    public Nature(Nature that)
    {
        this.positiveStat = that.positiveStat;
        this.negativeStat = that.negativeStat;
        this.favoriteFlavor = that.favoriteFlavor;
        this.dislikedFlavor = that.dislikedFlavor;
        this.natureName = that.natureName;
    }
}
