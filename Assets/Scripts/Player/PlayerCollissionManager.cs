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
            this.GetComponent<PlayerController>().IsInTallGrass = true;
            other.gameObject.GetComponent<GrassEncounterField>().RegisterToMovement(this.GetComponent<PlayerController>().StepsTakenInGrass);
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
