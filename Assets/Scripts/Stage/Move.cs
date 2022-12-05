using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int ToStage;
    GameObject PlayerPos;
    public Vector2 MoveSetPos;
    public Animator penelani;
    data stage;

    public bool asDoor = false;

    private void Start()
    {
        penelani = GameObject.Find("Ææ³Ú").GetComponent<Animator>();
        stage = GameObject.Find("Main Camera").GetComponent<data>();
        PlayerPos = GameObject.Find("Player");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && !asDoor)
        {
            penelani.SetTrigger("Ææ³Ú °¡µ¿");
            PlayerPos.transform.position = MoveSetPos;
            stage.Stage = ToStage;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player" && asDoor && Input.GetKey(KeyCode.Space))
        {
            penelani.SetTrigger("Ææ³Ú °¡µ¿");
            PlayerPos.transform.position = MoveSetPos;
            stage.Stage = ToStage;
        }
    }

}
