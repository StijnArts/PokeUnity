using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCameraScript : MonoBehaviour
{
    public GameObject target;
    private Vector3 originalPosition;
    //private Vector3 offset = new Vector3(0, 5, -7);

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        updatePosition();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        updatePosition();
    }

    private void updatePosition()
    {
        transform.position = target.transform.position + originalPosition;
    }
}
