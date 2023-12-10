using Assets.Scripts.Pokemon.Data;
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
        //RegisterPokemonForms();
        //RegisterPokemonTypes();
        //RegisterPokemonNatures();
        RegisterPokemonAbilities();
        RegisterPokemonExperienceGroups();
        GameStateManager.SetState(GameStateManager.GameStates.ROAMING);
        //Register forms to pokemon after they have been registered to only have to loop through all the files once.
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
        PokemonTypeRegistry.registerTypes();
        Debug.Log(PokemonTypeRegistry.registryToString());
    }

    private void RegisterPokemonForms()
    {
        string pokemonFormJsonDirectory = Path.GetFullPath("./") + "PokemonSpecies/Forms";
        foreach (string path in Directory.GetFiles(pokemonFormJsonDirectory))
        {
            PokemonForm pokemonForm = PokemonJsonReader.getPokemonData<PokemonForm>(path);
            if (pokemonForm != null)
            {
                string fileName = Path.GetFileName(path).Replace(".json", "");
                string pokemonId = fileName.Split('-')[0];
                string formId = fileName.Replace(pokemonId+"-","");
                var pokemon = PokemonRegistry.GetPokemonSpecies(pokemonId);
                if(pokemon != null)
                {
                    pokemon.Forms.Add(formId, pokemonForm);
                }
            }
        }
        Debug.Log(PokemonRegistry.registryToString());
    }

    private void RegisterPokemon()
    {
        PokemonRegistry.RegisterPokemonSpecies();
    }
}
