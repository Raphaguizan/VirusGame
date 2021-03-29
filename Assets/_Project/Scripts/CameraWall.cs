using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWall : MonoBehaviour
{
    [Tooltip("game object to follow")]
    public GameObject player;// objeto que a camera deve seguir
    [Tooltip("boundaries of the camera movement")]
    public GameObject upV, downV;// limite superior e inferior da câmera

    // Update is called once per frame
    void Update()
    {
        // ajusta a posição da camera na vertical
        if (player.transform.position.y < downV.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, downV.transform.position.y, transform.position.z);
        }
        else if (player.transform.position.y > upV.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, upV.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}