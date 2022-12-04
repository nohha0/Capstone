using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int ToStage;
    GameObject PlayerPos;
    public Vector2 MoveSetPos;

    data stage;

    public bool asDoor = false;

    private void Start()
    {
        stage = GameObject.Find("Main Camera").GetComponent<data>();
        PlayerPos = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && !asDoor)
        {
            PlayerPos.transform.position = MoveSetPos;
            stage.Stage = ToStage;
        }
        if (other.gameObject.CompareTag("Player") && asDoor)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PlayerPos.transform.position = MoveSetPos;
                stage.Stage = ToStage;
            }
        }
    }

}
