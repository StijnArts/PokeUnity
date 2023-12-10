using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Static : Ability
{
    public Static() : base(
        "static", "Static", 
        new List<string>()
        {
            "pikachu"
        }, 
        new List<string>())
    {
        
    }
}
