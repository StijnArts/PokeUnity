using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrassEncounterField : MonoBehaviour
{
    public GrassEncounterTable GrassEncounterTable;
    public SpawnConditions SpawnConditions;
    [HideInInspector]
    public GameObject PokemonPrefab;
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public GameObject PokemonGameObject;
    public void Start()
    {
        PokemonPrefab = Resources.Load("Prefabs/Npc's/Pokemon/Pokemon", typeof(GameObject)) as GameObject;
        Player = GameObject.Find("Player");
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
                if (stepsTakenInGrass.Value > 1 && PokemonGameObject.IsDestroyed())
                {
                    Debug.Log("Player took " + stepsTakenInGrass.Value + "steps.");
                    stepsTakenInGrass.SetValue(0);
                    PokemonSpawnEntry spawnEntry = GrassEncounterTable.pickFromSpawnEntries();
                    PokemonGameObject = Instantiate(PokemonPrefab, Player.transform.position, Quaternion.identity);
                    PokemonGameObject.GetComponent<PokemonNpc>().PokemonIndividualData = PokemonCreator.InstantiatePokemonForSpawn(spawnEntry, SpawnConditions);
                    PokemonGameObject.GetComponent<PokemonNpc>().InitializeSelf();
                    PokemonGameObject.GetComponent<BoxCollider>().enabled = false;//set location at player
                }
            }
            
        };
    }

    public void UnregisterToMovement(ObservableClasses.ObservableFloat stepsTakenInGrass)
    {
        stepsTakenInGrass.OnChanged -= TrackMovement(stepsTakenInGrass);
    }
}
