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

    float cameraHalfWidth, cameraHalfHeight;
    data stage;

    bool yUP = false;
    void Start()
    {
        stage = GetComponent<data>();
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }
    private void Update()
    {
        if (stage.Stage == 1)
        {
            minPosition = new Vector2(-239.1f, -65f);
            maxPosition = new Vector2(380.8f, 7.6f);
        }
        if (stage.Stage == 2)
        {
            minPosition = new Vector2(505.8f, -65f);
            maxPosition = new Vector2(1169.4f, 207.1f);
        }
        if (stage.Stage == 3)
        {
            minPosition = new Vector2(1307f, -64f);
            maxPosition = new Vector2(2043f, 222f);
        }
        if (stage.Stage == 4)
        {
            minPosition = new Vector2(2196f, -34f);
            maxPosition = new Vector2(3035f, 147.1f);
        }
        if (stage.Stage == 5)
        {
            minPosition = new Vector2(3363.6f, -1164.5f);
            maxPosition = new Vector2(3710f, -80f);
        }
        if (stage.Stage == 6)
        {
            minPosition = new Vector2(3121f, -80f);
            maxPosition = new Vector2(4083f, 302f);
        }
        if (stage.Stage == 7)
        {
            minPosition = new Vector2(4181f, -96f);
            maxPosition = new Vector2(5229f, 326f);
        }
        if (stage.Stage == 8)
        {
            minPosition = new Vector2(5327f, -107.9f);
            maxPosition = new Vector2(5631.4f, 28.7f);
        }
    }
    void LateUpdate()
    {
        if(yUP)
        {
            Vector3 playerPosition = new Vector3(player.position.x, player.position.y + space, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
        }
        if(transform.position != player.position&&!yUP)
        {
            //Vector3 playerPosition = new Vector3 (player.position.x, player.position.y + space, transform.position.z);
            //transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(player.position.x, minPosition.x + cameraHalfWidth, maxPosition.x - cameraHalfWidth),   // X
                Mathf.Clamp(player.position.y, minPosition.y + cameraHalfHeight, maxPosition.y - cameraHalfHeight), // Y
                -10);                                                                                                  // Z
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothing);
        }
        /*
        Vector3 PlayerPos = player.transform.position;
        transform.position = new Vector3
            (PlayerPos.x, PlayerPos.y + space, transform.position.z);
        */
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CameraSetPlatform"))
        {

            
        }
    }
}
