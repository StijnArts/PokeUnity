using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class Ability : UnityEngine.Object
{
    public enum Abilities { 
        
        None,
        [Description("static")]
        Static
    }
    public string AbilityName;
    public string AbilityId;
    public Abilities AbilityEnum;
    //should register observables on the pokemon when the pokemon is instantiated, and on the battle when the pokemon holding the ability enters the battle.
    
    public Ability(Abilities @enum, string id, string abilityName)
    {
        AbilityEnum = @enum;
        AbilityId = id;
        AbilityName = abilityName;
    }
}
