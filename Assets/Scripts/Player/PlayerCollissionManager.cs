using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.GetComponent<GrassEncounterField>() != null)
        {
            //Debug.Log("Found a grass field");
            this.GetComponent<PlayerController>().isInTallGrass = true;
            other.gameObject.GetComponent<GrassEncounterField>().RegisterToMovement(this.GetComponent<PlayerController>().stepsTakenInGrass);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<GrassEncounterField>() != null)
        {
            //Debug.Log("Found a grass field");
            this.GetComponent<PlayerController>().isInTallGrass = false;
            other.gameObject.GetComponent<GrassEncounterField>().UnregisterToMovement(this.GetComponent<PlayerController>().stepsTakenInGrass);
        }
    }
}
