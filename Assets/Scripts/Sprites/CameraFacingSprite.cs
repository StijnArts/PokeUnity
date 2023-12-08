using System.IO;
using UnityEngine;

public class CameraFacingSprite : MonoBehaviour
{

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
