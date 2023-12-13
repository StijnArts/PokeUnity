using Assets.Scripts.Battle;
using Assets.Scripts.Sprites;
using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PokemonNpc : MonoBehaviour
{
    public enum PokemonGender { MALE, FEMALE, NONE }
    public enum Stats { HP, Attack, Defence, Special_Attack, Special_Defence, Speed }

    [Serialize]
    public PokemonIndividualData PokemonIndividualData = new PokemonIndividualData();
    [SerializeField]
    public bool IsWild = false;
    void Start()
    {
        if (PokemonIndividualData.isSavedPokemon)
        {
            if (GameStateManager.GetState() != GameStateManager.GameStates.LOADING)
            {
                InitializeSelf();
            }
            else
            {
                GameStateManager.CurrentGameState.OnChanged += InitializeAfterLoading();
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
                GameStateManager.CurrentGameState.OnChanged -= InitializeAfterLoading();
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
        if (gameObject != null)
        {
            var pokemonSprite = gameObject.GetComponentInChildren<PokemonSprite>();
            if (pokemonSprite == null)
            {
                gameObject.AddComponent<PokemonSprite>();
            }
            pokemonSprite.enabled = false;
            pokemonSprite.InitializeSprite(pokemonId, PokemonIndividualData.GetSpriteSuffix(), PokemonIndividualData.GetSpriteWidth(), PokemonIndividualData.GetSpriteResolution(), PokemonIndividualData.GetSpriteAnimationSpeed());
            pokemonSprite.enabled = true;
        }
    }
}

