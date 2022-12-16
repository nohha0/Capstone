using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffTrigger : MonoBehaviour
{
    public bool hasAttacked = false;

    SpriteRenderer spriteRenderer;
    public ParticleSystem part;
    void Start()
    {
        part = GetComponent<ParticleSystem>();

        spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (hasAttacked)
        {
            spriteRenderer.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player" && !hasAttacked)
        {
            hasAttacked = true;
            Invoke("attackOn", 3);
            GameObject.Find("Player").GetComponent<CharacterStats>().TakeDamage();
        }
    }


    public void attackOn()
    {
        hasAttacked = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
