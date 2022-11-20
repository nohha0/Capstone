using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpray : MonoBehaviour
{
    [SerializeField] GameObject Small_Water;
    float time = 0.2f;
    void Start()
    {
        InvokeRepeating("SetTime", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= 0)
        {
            SetWater();
        }
        time -= Time.deltaTime;
    }

    void SetWater()
    {
        int Num = Random.Range(5, 12);
        for(int i =0; i < Num;i++)
        {
            Instantiate(Small_Water, transform.position, transform.rotation);
        }
    }
    
    void SetTime()
    {
        time = Random.Range(0.2f, 0.7f);
    }
    

}
