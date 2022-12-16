using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootingpaticle : MonoBehaviour
{
    public ParticleSystem Ps;

    bool Re = true;
    Transform scail;
    void Start()
    {
        scail = gameObject.transform;
        gameObject.tag = "Untagged";
    }

    void Update()
    {
        if(gameObject.activeSelf&&Re)
        {
            Ps.gameObject.SetActive(true);
            Re = false;
            Ps.Play();
            
            Invoke("Scailing", 1f);
        }
        if(gameObject.activeSelf)
        {
            bool Re = true;
            gameObject.tag = "Untagged";
            transform.localScale = scail.transform.localScale;
        }
    }
    void Scailing()
    {
        gameObject.tag = "Enemy";
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y+0.35f,transform.localScale.z);
    }
}
