 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikachu : PokemonSpecies
{
    public Pikachu() :
         base(
             "Pikachu",                                 //Name
             "pikachu",                                 //Pokemon Id
             25,                                        //National Pokedex Number
             "electric",                                //Primary Type passed by type id
                                                        //Secondary Type (leave empty if none)
             new List<string>() { "static" },           //Regular Abilities passed by ability id
             "lightning_rod",                           //Hidden Ability passed by ability id
             new BaseStats(35, 55, 40, 50, 50, 90),     //Base Stats
             190,                                       //Catchrate
             0.5,                                       //Male ratio
             112,                                       //Base Experience Yield
             "medium_fast",                             //Experience Group passed by experience group id
             10,                                        //Egg Cycles
             new List<string>() { "field", "fairy" },   //Egg Groups passed by egg group id
             50,                                        //Base Friendship
             new EvYield() { Speed = 2 },               //Ev Yield
             40,                                        //Height in Centimetres
             6.0                                        //Weight in Kilograms
       )
    {

    }
}
