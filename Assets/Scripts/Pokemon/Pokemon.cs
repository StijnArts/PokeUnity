using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using Unity.VisualScripting;
using Assets.Scripts.Pokemon.Data;

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
        string suffix = "";
        if (pokemonHasForm(pokemonId, formId))
        {
            suffix += "_" + formId;
        }
        if (pokemonOrFormHasGenderDifferences(pokemonId, formId) && gameObject.GetComponent<Pokemon>().PokemonIndividualData.gender == Pokemon.PokemonGender.FEMALE)
        {
            suffix += "_female";
        }
        if (gameObject.GetComponent<Pokemon>().PokemonIndividualData.isShiny)
        {
            suffix += "_shiny";
        }
        
        if (gameObject != null)
        {
            gameObject.GetComponentInChildren<CameraFacingSprite>().SetNewSpriteLocation(pokemonId, suffix);
        }
    }

    private bool pokemonOrFormHasGenderDifferences(string pokemonId, string formId)
    {
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
        return PokemonRegistry.GetPokemonSpecies(pokemonId).Forms.ContainsKey(formId);
    }
}
