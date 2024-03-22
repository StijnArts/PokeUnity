using Assets.Scripts.Battle;
using Assets.Scripts.Battle.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    private PlayerController _player => GameObject.Find("Player").GetComponent<PlayerController>();

    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.GetComponent<GrassEncounterField>() != null)
        {
            //Debug.Log("Found a grass field");
            this.GetComponent<PlayerController>().IsInTallGrass = true;
            other.gameObject.GetComponent<GrassEncounterField>().RegisterToMovement(this.GetComponent<PlayerController>().StepsTakenInGrass);
        }

        if(other.gameObject.GetComponent<PokemonNpc>() != null)
        {
            var pokemon = other.gameObject.GetComponent<PokemonNpc>();
            if (pokemon.IsWild && GameStateManager.CurrentGameState.Value == GameStateManager.GameStates.ROAMING &&
                _player.Party.GetSelectedPokemon() != null)
            {
                //Start Battle in battle manager
                /*ServiceLocator.Instance.BattleManager.StartBattle(_player.SelectPokemon(), new List<List<PokemonIndividualData>>() { new List<PokemonIndividualData>() { pokemon.PokemonIndividualData } });*/
                ServiceLocator.Instance.BattleManager.StartBattle(new PlayerBattleController(_player.SelectPokemon()), new List<BattleController>() { 
                    new NpcBattleController(new List<PokemonIndividualData>{ pokemon.PokemonIndividualData })});
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<GrassEncounterField>() != null)
        {
            //Debug.Log("Found a grass field");
            this.GetComponent<PlayerController>().IsInTallGrass = false;
            other.gameObject.GetComponent<GrassEncounterField>().UnregisterToMovement(this.GetComponent<PlayerController>().StepsTakenInGrass);
        }
    }
}
