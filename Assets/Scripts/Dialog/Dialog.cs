using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{
    //public enum DialogType {ITEM_GIVING, POKEMON_GIVING, OPTION}
    //[SerializeField]
    //public List<Flag> flags = new List<Flag>();
    [SerializeField]
    public string dialogTitle = "";
    [TextArea(3,10)]
    [SerializeField]
    public string text = "";
    [SerializeField]
    public bool isSpecial = false;
    [SerializeField]
    public bool giftsItems = false;
    [SerializeField]
    public bool giftsPokemon = false;
    [SerializeField]
    public bool HasBattle = false;
    [SerializeField]
    public bool HasOptions = false;
    [SerializeField]
    public Party battleParty = new Party();
    [SerializeField]
    public bool setsFlags = false;
    [SerializeField]
    public ItemTransferEntry[] giftableItems = new ItemTransferEntry[0];
    [SerializeField]
    public PokemonGiftEntry[] giftablePokemon = new PokemonGiftEntry[0];
    [HideInInspector]
    public bool actionsHaveBeenExecuted = false;
}
