using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 3;
    public Vector2 offset;
    public float limitMinX, limitMaxX, limitMinY, limitMaxY;
    float cameraHalfWidth, cameraHalfHeight;

    data stage;
    private void Start()
    {
        stage = GetComponent<data>();
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }
    private void Update()
    {
        if (stage.Stage == 1)
        {
            limitMinX = -239.1f;
            limitMaxX = 380.8f;
            limitMinY = -65;
            limitMaxY = 172.3f;
        }
        if (stage.Stage == 2)
        {
            limitMinX = 505.8f;
            limitMaxX = 1169.4f;
            limitMinY = -65;
            limitMaxY = 207.1f;
        }
        if (stage.Stage == 3)
        {
            limitMinX = 1307f;
            limitMaxX = 2043;
            limitMinY = -64;
            limitMaxY = 222;
        }
        if (stage.Stage == 4)
        {
            limitMinX = 2196;
            limitMaxX = 3055.1f;
            limitMinY = -34.4f;
            limitMaxY = 147.1f;
        }
        if (stage.Stage == 5)
        {
            limitMinX = 3363.6f;
            limitMaxX = 3710.6f;
            limitMinY = -1164.5f;
            limitMaxY = -80;
        }
        if (stage.Stage == 6)
        {
            limitMinX = 3121;
            limitMaxX = 4045.4f;
            limitMinY = -80;
            limitMaxY = 302;
        }
        if (stage.Stage == 7)
        {
            limitMinX = 4181;
            limitMaxX = 5229;
            limitMinY = -96;
            limitMaxY = 326;
        }
        if (stage.Stage == 8)
        {
            limitMinX = 5327f;
            limitMaxX = 5589.8f;
            limitMinY = -107.9f;
            limitMaxY = 22.1f;
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, limitMinY + cameraHalfHeight, limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }
}