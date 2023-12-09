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
                initializeSelf();
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
                initializeSelf();
                GameStateManager.currentGameState.OnChanged -= InitializeAfterLoading();
            }
        };
    }

    public void initializeSelf()
    {
        //PokemonIndividualData.natureData = PokemonNatureRegistry.GetNature((int)PokemonIndividualData.nature);
        //PokemonIndividualData.abilityData = AbilityRegistry.GetAbility(PokemonIndividualData.Ability);
        //PokemonIndividualData.primaryType = PokemonTypeRegistry.GetType(PokemonRegistry.GetPokemon(PokemonIndividualData.pokemonId).PrimaryType);
        //string secondaryTypeName = PokemonRegistry.GetPokemon(PokemonIndividualData.pokemonId).SecondaryType;
        //if (secondaryTypeName != null)
        //{
        //    PokemonIndividualData.secondaryType = PokemonTypeRegistry.GetType(secondaryTypeName);
        //}

        //PokemonIndividualData.stats = PokemonIndividualData.calculateStats();
        InitializeSprite(PokemonIndividualData.pokemonId, PokemonIndividualData.formId);
    }

    public void setIndividualPokemonData(PokemonIndividualData pokemonIndividualData)
    {
        this.PokemonIndividualData = pokemonIndividualData;
        initializeSelf();
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
            return PokemonRegistry.GetPokemon(pokemonId).Forms[formId].HasGenderDifferences;
        }
        else
        {
            return PokemonRegistry.GetPokemon(pokemonId).HasGenderDifferences;
        }
    }

    private static bool pokemonHasForm(string pokemonId, string formId)
    {
        return false;//PokemonRegistry.GetPokemon(pokemonId).Forms.ContainsKey(formId);
    }
}
