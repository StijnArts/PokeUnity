using Assets.Scripts.Battle;
using Assets.Scripts.Battle.Conditions;
using Assets.Scripts.Battle.Effects;
using Assets.Scripts.Battle.Events;
using Assets.Scripts.Battle.Events.Sources;
using Assets.Scripts.Pokemon;
using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Pokemon.Data.Moves;
using Assets.Scripts.Pokemon.PokemonTypes;
using Assets.Scripts.Registries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class PokemonIndividualData : Target, BattleEventSource, EffectHolder, SpeedSortable
{
    public string PokemonId;
    public string PokemonName;
    [HideInInspector]
    public PokemonSpecies BaseSpecies;
    public ObservableClasses.ObservableInteger Level = new ObservableClasses.ObservableInteger() { Value = 1 };//TODO make move learning and evolutions subscribe to the level observable
    public string Nickname = null;
    [HideInInspector]
    public int? CurrentHp;
    public string FormId = "";
    public MoveSlot[] Moves = new MoveSlot[Settings.MaxMoveSlots];
    public string Ability;
    [HideInInspector]
    public Nature.Natures Nature = global::Nature.Natures.Adamant;
    [HideInInspector]
    public Nature NatureData;
    [HideInInspector]
    public float HitboxHeight;
    [HideInInspector]
    public float HitboxWidth;
    public bool IsShiny = false;
    public PokemonNpc.PokemonGender gender = PokemonNpc.PokemonGender.MALE;
    [HideInInspector]
    public PokemonStats Stats = new PokemonStats(1, 1, 1, 1, 1, 1);
    [HideInInspector]
    public PokemonStats BaseStats = new PokemonStats(1, 1, 1, 1, 1, 1);
    public PokemonEVs EVs = new PokemonEVs();
    public PokemonIVs IVs = new PokemonIVs();
    public string Item;
    [HideInInspector]
    public bool IsValid = false;
    public int CurrentExperience = 0;
    public int Friendship = 0;
    public List<Move> LearnableMoves = new List<Move>();
    public bool IsSavedPokemon = false;
    [HideInInspector]
    public PokemonBattleData BattleData;
    public string Status;
    public bool Fainted => CurrentHp >= 0;

    public string GetSpriteSuffix()
    {
        string suffix = "";
        if (pokemonHasForm(BaseSpecies, FormId))
        {
            suffix += "_" + FormId;
        }
        if (pokemonOrFormHasGenderDifferences(BaseSpecies, FormId) && gender == PokemonNpc.PokemonGender.FEMALE)
        {
            suffix += "_female";
        }
        if (IsShiny)
        {
            suffix += "_shiny";
        }
        return suffix;
    }

    private bool pokemonOrFormHasGenderDifferences(PokemonSpecies pokemonSpecies, string formId)
    {
        if (pokemonHasForm(pokemonSpecies, formId))
        {
            return pokemonSpecies.Forms[formId].HasGenderDifferences;
        }
        else
        {
            return pokemonSpecies.HasGenderDifferences;
        }
    }

    private static bool pokemonHasForm(PokemonSpecies pokemonSpecies, string formId)
    {
        return pokemonSpecies.Forms.ContainsKey(formId);
    }

    private PokemonStats CalculateStats(int level, PokemonEVs pokemonEVs, PokemonIVs pokemonIVs, Nature nature)
    {
        BaseStats baseStats = BaseSpecies.BaseStats;

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
        BaseStats baseStats = BaseSpecies.BaseStats;
        int hp = PokemonStats.CalculateHp(Level.Value, baseStats.Hp, EVs.hpEVs, IVs.hpIVs);
        int attack = PokemonStats.CalculateOtherStat(Level.Value, baseStats.Attack, EVs.attackEVs,
            IVs.attackIVs, NatureData, global::Nature.AffectedStats.ATTACK);
        int defence = PokemonStats.CalculateOtherStat(Level.Value, baseStats.Defence, EVs.defenceEVs,
            IVs.defenceIVs, NatureData, global::Nature.AffectedStats.DEFENCE);
        int specialAttack = PokemonStats.CalculateOtherStat(Level.Value, baseStats.SpecialAttack, EVs.specialAttackEVs,
            IVs.specialAttackIVs, NatureData, global::Nature.AffectedStats.SPECIAL_ATTACK);
        int specialdefence = PokemonStats.CalculateOtherStat(Level.Value, baseStats.SpecialDefence, EVs.specialdefenceEVs,
            IVs.specialdefenceIVs, NatureData, global::Nature.AffectedStats.SPECIAL_DEFENCE);
        int speed = PokemonStats.CalculateOtherStat(Level.Value, baseStats.Speed, EVs.speedEVs,
            IVs.speedIVs, NatureData, global::Nature.AffectedStats.SPEED);

        return new PokemonStats(hp, attack, defence, specialAttack, specialdefence, speed);
    }

    internal void Initialize()
    {
        NatureData = PokemonNatureRegistry.GetNature((int)Nature);

        BaseSpecies = PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId));
        
        Stats = CalculateStats();

        if (!string.IsNullOrEmpty(FormId) && BaseSpecies.Forms.ContainsKey(FormId))
        {
            //TODO overwrite species data where form data is not null
        }
    }

    public int GetSpriteWidth()
    {
        return PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId)).SpriteWidth;
    }

    public int GetSpriteResolution()
    {
        return PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId)).SpriteResolution;
    }

    public int GetSpriteAnimationSpeed()
    {
        return PokemonRegistry.GetPokemonSpecies(new PokemonIdentifier(PokemonId, FormId)).SpriteAnimationSpeed;
    }

    public string GetName()
    {
        return string.IsNullOrEmpty(Nickname) ? PokemonName : Nickname;
    }

    public void SetBattleData(BattleController controller, Battle battle)
    {
        BattleData = new PokemonBattleData(this, controller, battle);
    }

    public List<PokemonIndividualData> GetAllyAndSelf()
    {
        return BattleData.BattleController.ActivePokemon.Select(pokemon => pokemon.PokemonIndividualData).ToList();
    }

    public List<PokemonIndividualData> GetFoes(Dictionary<BattleController, List<PokemonNpc>> activePokemon)
    {
        return activePokemon.Where(pair=>pair.Key != BattleData.BattleController).SelectMany(pair => pair.Value).Select(pokemon => pokemon.PokemonIndividualData).ToList();
    }

    public Condition GetStatus()
    {
        return ConditionRegistry.GetConditionById(Status);
    }

    public bool ClearStatus()
    {
        if(CurrentHp == null || Status == null) return false;
        if (Status == "slp" && RemoveVolatile("nightmare")){
            //TODO send message in battle dialog;
        }
        Status = "";
        return true;
    }

    public bool RemoveVolatile(string volatileStatusId)
    {
        if(CurrentHp == null) return false;
        var statusEffect = ConditionRegistry.GetConditionById(volatileStatusId);
        if (statusEffect == null) return false;
        var volatileStatus = BattleData.Volatiles[statusEffect.Id];
        BattleData.Battle.SingleEvent("End", statusEffect, volatileStatus, this);
        var linkedPokemon = volatileStatus["LinkedPokemon"];
        var linkedStatus = volatileStatus["LinkedStatus"];
        BattleData.Volatiles.Remove(statusEffect.Id);
        if( linkedPokemon != null)
        {
            if(linkedStatus is string)
            {
                RemoveLinkedVolatiles((string)linkedStatus, null, (List<PokemonIndividualData>)linkedPokemon);
            } else if(linkedStatus is Effect)
            {
                RemoveLinkedVolatiles(null, (Effect)linkedStatus, (List<PokemonIndividualData>)linkedPokemon);
            }

        }
        return true;
    }

    public void RemoveLinkedVolatiles(string linkedStatusString = null, Effect linkedStatusEffect = null, List<PokemonIndividualData> linkedPokemon = null)
    {
        if(linkedPokemon == null) throw new ArgumentNullException(nameof(linkedPokemon) + " cannot be null");
        string linkedStatus = null;
        if (linkedStatusString != null) linkedStatus = linkedStatusString;
        else if (linkedStatusEffect != null) linkedStatus = linkedStatusEffect.Id;
        else throw new ArgumentNullException("linkedStatus cannot be null");
        foreach(var linkedPoke in linkedPokemon)
        {
            var volatileData = linkedPoke.BattleData.Volatiles[linkedStatus];
            if(volatileData == null) continue;
            var volatileDataLinkedPokemon = (List<PokemonIndividualData>)volatileData["LinkedPokemon"];
            volatileDataLinkedPokemon.Remove(this);
            volatileData["LinkedPokemon"] = volatileDataLinkedPokemon;
            if (volatileDataLinkedPokemon.Count == 0)
            {
                linkedPoke.RemoveVolatile(linkedStatus);
            }
        }
    }

    public double GetStat(PokemonStats.StatTypes statType, bool unboosted, bool unmodified) => BattleData.GetStat(this, statType, unboosted, unmodified);

    public string ClearAbility()
    {
        return SetAbility("");
    }

    public string SetAbility(string abilityAsString = null, Ability abilityAsAbility = null, PokemonIndividualData source = null, bool isFromFormeChange = false, bool isTransform = false)
    {
        if (CurrentHp < 1) return null;
        Ability ability = null;
        if (abilityAsAbility != null) ability = abilityAsAbility;
        else if (abilityAsString != null) ability = AbilityRegistry.GetAbility(abilityAsString);
        else throw new ArgumentNullException("ability cannot Be null");

        var oldAbility = Ability;
        if (!isFromFormeChange)
        {
            if (ability.IsPermanent || GetAbility().IsPermanent) return null;
        }
        var battleEffect = BattleData.Battle.Effect;
        if (!isFromFormeChange && !isTransform)
        {
            bool? setAbilityEvent = (bool?)BattleData.Battle.RunEvent("SetAbility", new List<Target>() { this }, source, battleEffect, ability);
            if (!setAbilityEvent.HasValue || !setAbilityEvent.Value) return null;
        }
        BattleData.Battle.SingleEvent("End", AbilityRegistry.GetAbility(oldAbility), BattleData.AbilityState, this, source);
        if(battleEffect != null && battleEffect.EffectType == EffectType.Move && !isFromFormeChange)
        {
            //TODO show message in dialog box
        }
        Ability = ability.Id;
        BattleData.AbilityState = new EffectState { { ability.Id, this } };
        if (ability.Id != null && (!isTransform || oldAbility != ability.Id))
        {
            BattleData.Battle.SingleEvent("Start", ability, BattleData.AbilityState, this, source);
        }
        BattleData.AbilityOrder = BattleData.Battle.AbilityOrder++;
        return oldAbility;
    }

    public bool IgnoringItem()
    {
        return !BattleData.Battle.GetActivePokemonIndividualData().Contains(this) 
            //TODO replace check for klutz check for general disabled item check on ability
            || (this.GetItem().IgnoresKlutz && HasAbility("klutz")) 
            || BattleData.Volatiles.ContainsKey("embargo") 
            || BattleData.Battle.Field.PseudoWeathers.ContainsKey("magicroom")
            ;
    }

    public Item GetItem()
    {
        return ItemRegistry.GetItemById(Item);
    }

    public bool HasAbility(List<string> abilityId)
    {
        return abilityId.All(ability => ability.Equals(GetAbility().Id));
    }

    public bool HasAbility(string abilityId)
    {
        if(abilityId != GetAbility().Id) return false;
        return !IgnoringAbility();
    }

    public bool IgnoringAbility()
    {
        if (GetAbility().IsPermanent) return false;
        if (BattleData.Volatiles.ContainsKey("gastroacid")) return true;

        if (HasItem("Ability Shield") || GetAbility().Id.Equals("neutralizinggas")) return false;
        foreach(var pokemon in BattleData.Battle.GetActivePokemonIndividualData())
        {
            if(pokemon.GetAbility().Id.Equals("neutralizinggas") && !pokemon.BattleData.Volatiles.ContainsKey("gastroacid")
                && !pokemon.BattleData.Transformed && !pokemon.BattleData.AbilityState.ContainsKey("ending") && !BattleData.Volatiles.ContainsKey("commanding"))
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItem(List<string> items)
    {
        return items.All(item => item.Equals(Item));
    }

    public bool HasItem(string item)
    {
        if(!item.Equals(Item)) return false;
        return !IgnoringItem();
    }

    public Ability GetAbility()
    {
        return AbilityRegistry.GetAbility(Ability);
    }

    public bool ClearItem()
    {
        return SetItem("");
    }

    public bool SetItem(string itemAsString = null, Item itemAsItem = null,  PokemonIndividualData source = null, Effect effect = null)
    {
        if (CurrentHp < 1 || !BattleData.Battle.GetActivePokemonIndividualData().Contains(this)) return false;
        if (BattleData.ItemState.ContainsKey("knockedOff")) return false;
        Item item = null;
        if (itemAsItem != null) item = itemAsItem;
        else if (itemAsString != null) item = ItemRegistry.GetItemById(itemAsString);
        else throw new ArgumentNullException("item cannot Be null");

        var oldItem = this.GetItem();
        var oldItemState = BattleData.ItemState;
        Item = item.Id;
        BattleData.ItemState = new EffectState { { item.Id, this } };
        if (oldItem.Exists) BattleData.Battle.SingleEvent("End", oldItem, oldItemState, this);
        if(item.Id != null)
        {
            BattleData.Battle.SingleEvent("Start", item, BattleData.ItemState, this, source, effect);
        }
        return true;
    }

    public int GetSpeed()
    {
        return BattleData.BattleSpeed;
    }

    public void UpdateSpeed()
    {
        BattleData.UpdateSpeed(this);
    }

    public void DisableMove(string moveId, bool? isHidden = null, Effect sourceEffect = null)
    {
        BattleData.DisableMove(moveId, isHidden, sourceEffect);
    }

    public string[] GetTypes(bool? excludeAdded = null, bool? preterastallized = null)
    {
        if (preterastallized == false && BattleData.IsTerastallized) return new string[] { BattleData.Terastallized };
        var types = (string[])BattleData.Battle.RunEvent("Type", new List<Target>() { this }, null, null, BattleData.Types);
        if (excludeAdded == false && BattleData.AddedType != null)
        {
            types.AddRange(BattleData.AddedType);
            return types.ToArray();
        }
        if (types.Length > 0) return types;
        return new string[] { Normal.TypeName };
    }
}
