using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffTrigger : MonoBehaviour
{
    ParticleSystem m;
    ParticleSystem.Particle[] mP;

    void Start()
    {

        m = GetComponent<ParticleSystem>();
        mP = new ParticleSystem.Particle[m.main.maxParticles];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D ot)
    {
        if(ot.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Player").GetComponent<CharacterStats>().TakeDamage();
        }
    }
}
