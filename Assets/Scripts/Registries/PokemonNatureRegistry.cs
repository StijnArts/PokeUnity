using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PokemonNatureRegistry
{


    public static Dictionary<int, Nature> PokemonNatures = new Dictionary<int, Nature>();

    public static Nature GetNature(int id)
    {
        Nature value;
        PokemonNatures.TryGetValue(id, out value);
        return new Nature(value);
    }

    public static void registerNatures()
    {
        PokemonNatures.Add((int)Nature.Natures.Adamant, new Nature("adamant", Nature.AffectedStats.ATTACK, Nature.AffectedStats.SPECIAL_ATTACK, Nature.Flavor.SPICY, Nature.Flavor.DRY));
        PokemonNatures.Add((int)Nature.Natures.Bashful, new Nature("bashful", Nature.AffectedStats.NEUTRAL, Nature.AffectedStats.NEUTRAL, Nature.Flavor.NEUTRAL, Nature.Flavor.NEUTRAL));
        PokemonNatures.Add((int)Nature.Natures.Brave, new Nature("brave", Nature.AffectedStats.ATTACK, Nature.AffectedStats.SPEED, Nature.Flavor.SPICY, Nature.Flavor.SWEET));
        PokemonNatures.Add((int)Nature.Natures.Bold, new Nature("bold", Nature.AffectedStats.DEFENCE, Nature.AffectedStats.ATTACK, Nature.Flavor.SOUR, Nature.Flavor.SPICY));
        PokemonNatures.Add((int)Nature.Natures.Calm, new Nature("calm", Nature.AffectedStats.SPECIAL_DEFENCE, Nature.AffectedStats.ATTACK, Nature.Flavor.BITTER, Nature.Flavor.SPICY));
        PokemonNatures.Add((int)Nature.Natures.Careful, new Nature("careful", Nature.AffectedStats.SPECIAL_DEFENCE, Nature.AffectedStats.SPECIAL_ATTACK, Nature.Flavor.BITTER, Nature.Flavor.DRY));
        PokemonNatures.Add((int)Nature.Natures.Docile, new Nature("docile", Nature.AffectedStats.NEUTRAL, Nature.AffectedStats.NEUTRAL, Nature.Flavor.NEUTRAL, Nature.Flavor.NEUTRAL));
        PokemonNatures.Add((int)Nature.Natures.Gentle, new Nature("gentle", Nature.AffectedStats.SPECIAL_DEFENCE, Nature.AffectedStats.DEFENCE, Nature.Flavor.BITTER, Nature.Flavor.SOUR));
        PokemonNatures.Add((int)Nature.Natures.Hardy, new Nature("hardy", Nature.AffectedStats.NEUTRAL, Nature.AffectedStats.NEUTRAL, Nature.Flavor.NEUTRAL, Nature.Flavor.NEUTRAL));
        PokemonNatures.Add((int)Nature.Natures.Hasty, new Nature("hasty", Nature.AffectedStats.SPEED, Nature.AffectedStats.DEFENCE, Nature.Flavor.SWEET, Nature.Flavor.SOUR));
        PokemonNatures.Add((int)Nature.Natures.Impish, new Nature("impish", Nature.AffectedStats.DEFENCE, Nature.AffectedStats.SPECIAL_ATTACK, Nature.Flavor.SOUR, Nature.Flavor.DRY));
        PokemonNatures.Add((int)Nature.Natures.Jolly, new Nature("jolly", Nature.AffectedStats.SPEED, Nature.AffectedStats.SPECIAL_ATTACK, Nature.Flavor.SWEET, Nature.Flavor.DRY));
        PokemonNatures.Add((int)Nature.Natures.Lax, new Nature("lax", Nature.AffectedStats.DEFENCE, Nature.AffectedStats.SPECIAL_DEFENCE, Nature.Flavor.SOUR, Nature.Flavor.BITTER));
        PokemonNatures.Add((int)Nature.Natures.Lonely, new Nature("lonely", Nature.AffectedStats.ATTACK, Nature.AffectedStats.DEFENCE, Nature.Flavor.SPICY, Nature.Flavor.SOUR));
        PokemonNatures.Add((int)Nature.Natures.Mild, new Nature("mild", Nature.AffectedStats.SPECIAL_ATTACK, Nature.AffectedStats.DEFENCE, Nature.Flavor.DRY, Nature.Flavor.SOUR));
        PokemonNatures.Add((int)Nature.Natures.Modest, new Nature("modest", Nature.AffectedStats.SPECIAL_ATTACK, Nature.AffectedStats.ATTACK, Nature.Flavor.DRY, Nature.Flavor.SPICY));
        PokemonNatures.Add((int)Nature.Natures.Naive, new Nature("naive", Nature.AffectedStats.SPEED, Nature.AffectedStats.SPECIAL_DEFENCE, Nature.Flavor.SWEET, Nature.Flavor.BITTER));
        PokemonNatures.Add((int)Nature.Natures.Naughty, new Nature("naughty", Nature.AffectedStats.ATTACK, Nature.AffectedStats.SPECIAL_DEFENCE, Nature.Flavor.SPICY, Nature.Flavor.BITTER));
        PokemonNatures.Add((int)Nature.Natures.Quiet, new Nature("quiet", Nature.AffectedStats.SPECIAL_ATTACK, Nature.AffectedStats.SPEED, Nature.Flavor.DRY, Nature.Flavor.SWEET));
        PokemonNatures.Add((int)Nature.Natures.Quirky, new Nature("quirky", Nature.AffectedStats.NEUTRAL, Nature.AffectedStats.NEUTRAL, Nature.Flavor.NEUTRAL, Nature.Flavor.NEUTRAL));
        PokemonNatures.Add((int)Nature.Natures.Rash, new Nature("rash", Nature.AffectedStats.SPECIAL_ATTACK, Nature.AffectedStats.SPECIAL_DEFENCE, Nature.Flavor.DRY, Nature.Flavor.BITTER));
        PokemonNatures.Add((int)Nature.Natures.Relaxed, new Nature("relaxed", Nature.AffectedStats.DEFENCE, Nature.AffectedStats.SPEED, Nature.Flavor.SOUR, Nature.Flavor.SWEET));
        PokemonNatures.Add((int)Nature.Natures.Sassy, new Nature("sassy", Nature.AffectedStats.SPECIAL_DEFENCE, Nature.AffectedStats.SPEED, Nature.Flavor.BITTER, Nature.Flavor.SWEET));
        PokemonNatures.Add((int)Nature.Natures.Serious, new Nature("serious", Nature.AffectedStats.NEUTRAL, Nature.AffectedStats.NEUTRAL, Nature.Flavor.NEUTRAL, Nature.Flavor.NEUTRAL));
        PokemonNatures.Add((int)Nature.Natures.Timid, new Nature("timid", Nature.AffectedStats.SPEED, Nature.AffectedStats.ATTACK, Nature.Flavor.SWEET, Nature.Flavor.SPICY));
    }
}
