using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public Transform play;
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        play = GameObject.Find("Player").transform;
        Invoke("OnDestroy", 0.15f);


        //Debug.Log("¥ÎΩ√¿Ã∆Â∆Æ");
    }


    // Update is called once per frame
    void Update()
    {

        if (sprite.flipX)
        {
            transform.position = new Vector2(play.position.x - 5, play.position.y);
        }
        if (!sprite.flipX)
        {
            transform.position = new Vector2(play.position.x + 5, play.position.y);
        }
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
