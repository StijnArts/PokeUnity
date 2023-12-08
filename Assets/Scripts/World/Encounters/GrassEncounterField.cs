using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassEncounterField : MonoBehaviour
{
    public GrassEncounterTable GrassEncounterTable;
    public SpawnConditions SpawnConditions;
    public GameObject pokemonPrefab;
    public GameObject player;
    public GameObject pokemonGameObject;
    public void Start()
    {
        pokemonPrefab = Resources.Load("Prefabs/Npc's/Pokemon/Pokemon", typeof(GameObject)) as GameObject;
        player = GameObject.Find("Player");
    }

    public void RegisterToMovement(ObservableClasses.ObservableFloat stepsTakenInGrass)
    {
        stepsTakenInGrass.OnChanged += TrackMovement(stepsTakenInGrass);
    }

    public Action TrackMovement(ObservableClasses.ObservableFloat stepsTakenInGrass)
    {
        return () => {
            if (GameStateManager.GetState() == GameStateManager.GameStates.ROAMING)
            {
                if (stepsTakenInGrass.Value > 1 && pokemonGameObject.IsDestroyed())
                {
                    Debug.Log("Player took " + stepsTakenInGrass.Value + "steps.");
                    stepsTakenInGrass.SetValue(0);
                    PokemonSpawnEntry spawnEntry = GrassEncounterTable.pickFromSpawnEntries();
                    pokemonGameObject = Instantiate(pokemonPrefab, player.transform.position, Quaternion.identity);
                    pokemonGameObject.GetComponent<Pokemon>().PokemonIndividualData = PokemonCreator.InstantiatePokemonForSpawn(spawnEntry, SpawnConditions);
                    pokemonGameObject.GetComponent<Pokemon>().initializeSelf();
                    pokemonGameObject.GetComponent<BoxCollider>().enabled = false;//set location at player
                }
            }
            
        };
    }

    public void UnregisterToMovement(ObservableClasses.ObservableFloat stepsTakenInGrass)
    {
        stepsTakenInGrass.OnChanged -= TrackMovement(stepsTakenInGrass);
    }
}
