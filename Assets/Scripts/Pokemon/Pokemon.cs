using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;

[Serializable]
public class Pokemon : MonoBehaviour
{
    public enum PokemonGender { MALE, FEMALE, NONE }
    public enum Stats { HP, Attack, Defence, Special_Attack,  Special_Defence, Speed }

    [Serialize]
    public PokemonIndividualData PokemonIndividualData = new PokemonIndividualData();
    void Start()
    {
        if (PokemonIndividualData.isSavedPokemon)
        {
            if(GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
            {
                InitializeSelf();
            } else
            {
                GameStateManager.currentGameState.OnChanged += InitializeAfterLoading();
            }
        }
    }

    private Action InitializeAfterLoading()
    {
        return () =>
        {
            if (GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
            {
                InitializeSelf();
                GameStateManager.currentGameState.OnChanged -= InitializeAfterLoading();
            }
        };
    }

    public void InitializeSelf()
    {
        PokemonIndividualData.Initialize();
        InitializeSprite(PokemonIndividualData.PokemonId, PokemonIndividualData.FormId);
    }

    public void SetIndividualPokemonData(PokemonIndividualData pokemonIndividualData)
    {
        PokemonIndividualData = pokemonIndividualData;
        InitializeSelf();
    }

    public void InitializeSprite(string pokemonId, string formId)
    {
        string spriteLocation = "Sprites/Pokemon/" + pokemonId + "/" + pokemonId;
        if (pokemonHasForm(pokemonId, formId))
        {
            spriteLocation += "_" + formId;
        }
        if (pokemonOrFormHasGenderDifferences(pokemonId, formId) && gameObject.GetComponent<Pokemon>().PokemonIndividualData.gender == Pokemon.PokemonGender.FEMALE)
        {
            spriteLocation += "_female";
        }
        if (gameObject.GetComponent<Pokemon>().PokemonIndividualData.isShiny)
        {
            spriteLocation += "_shiny";
        }
        
        if (gameObject != null)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load(spriteLocation+"_animated", typeof(Sprite)) as Sprite;
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    private bool pokemonOrFormHasGenderDifferences(string pokemonId, string formId)
    {
        return false;
        if (pokemonHasForm(pokemonId, formId))
        {
            return PokemonRegistry.GetPokemonSpecies(pokemonId).Forms[formId].HasGenderDifferences;
        }
        else
        {
            return PokemonRegistry.GetPokemonSpecies(pokemonId).HasGenderDifferences;
        }
    }

    private static bool pokemonHasForm(string pokemonId, string formId)
    {
        return false;//PokemonRegistry.GetPokemon(pokemonId).Forms.ContainsKey(formId);
    }
}
