using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalse : MonoBehaviour
{
    PlayerController Move;
    void Start()
    {
        Move = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(gameObject.activeSelf) Move.movable = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            Move.movable = true;
        }
    }
}
