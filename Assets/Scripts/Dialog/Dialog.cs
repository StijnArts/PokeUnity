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
    public string DialogTitle = "";
    [TextArea(3,10)]
    [SerializeField]
    public string Text = "";
    [SerializeField]
    public bool IsSpecial = false;
    [SerializeField]
    public bool GiftsItems = false;
    [SerializeField]
    public bool GiftsPokemon = false;
    [SerializeField]
    public bool HasBattle = false;
    [SerializeField]
    public bool HasOptions = false;
    [SerializeField]
    public Party BattleParty = new Party();
    [SerializeField]
    public bool SetsFlags = false;
    [SerializeField]
    public ItemTransferEntry[] GiftableItems = new ItemTransferEntry[0];
    [SerializeField]
    public PokemonGiftEntry[] GiftablePokemon = new PokemonGiftEntry[0];
    [HideInInspector]
    public bool actionsHaveBeenExecuted = false;
}
