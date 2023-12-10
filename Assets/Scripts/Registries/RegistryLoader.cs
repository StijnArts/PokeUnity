using Assets.Scripts.Pokemon.Data;
using Assets.Scripts.Registries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RegistryLoader : MonoBehaviour
{
    //public ObservableClasses.ObservableBoolean isLoading = new ObservableClasses.ObservableBoolean() {Value = true};
    // Start is called before the first frame update
    void Start()
    {
        RegisterPokemon();
        RegisterPokemonForms();
        RegisterPokemonTypes();
        RegisterPokemonNatures();
        RegisterPokemonAbilities();
        RegisterPokemonExperienceGroups();
        RegisterMoves();
        RegisterMoveSets();
        RegisterPokedexes();
        GameStateManager.SetState(GameStateManager.GameStates.ROAMING);
        //Register forms to pokemon after they have been registered to only have to loop through all the files once.
    }

    private void RegisterPokedexes()
    {
        PokedexRegistry.RegisterPokedexes();
    }

    private void RegisterMoveSets()
    {
        MoveRegistry.RegisterMoveSets();
    }

    private void RegisterMoves()
    {
        MoveRegistry.RegisterMoves();
    }

    private void RegisterPokemonAbilities()
    {
        AbilityRegistry.RegisterAbilities();
    }

    private void RegisterPokemonExperienceGroups()
    {
        ExperienceGroupRegistry.registerExperienceGroups();
        ExperienceGroupRegistry.testExperienceGroups();
    }

    private void RegisterPokemonNatures()
    {
        PokemonNatureRegistry.registerNatures();
    }

    private void RegisterPokemonTypes()
    {
        PokemonTypeRegistry.RegisterTypes();
        Debug.Log(PokemonTypeRegistry.registryToString());
    }

    private void RegisterPokemonForms()
    {
        PokemonRegistry.RegisterPokemonForms();
        Debug.Log(PokemonRegistry.registryToString());
    }

    private void RegisterPokemon()
    {
        PokemonRegistry.RegisterPokemonSpecies();
    }
}
