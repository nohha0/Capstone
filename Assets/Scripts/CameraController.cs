using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float space;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    void LateUpdate()
    {
        if(transform.position != player.position)
        {
            Vector3 playerPosition = new Vector3 (player.position.x, player.position.y + space, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
        }
        /*
        Vector3 PlayerPos = player.transform.position;
        transform.position = new Vector3
            (PlayerPos.x, PlayerPos.y + space, transform.position.z);
        */
    }
}
