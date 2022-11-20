using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWater : MonoBehaviour
{
    [SerializeField] protected GameObject Big_Water;
    [SerializeField] Transform Base;
    SpriteRenderer rend;
    Vector3 Pos;
    int a = 0;

    bool Skill_SprayWater = false;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Pos = new Vector3(Base.position.x + 4, Base.position.y + 55, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Skill_SprayWater)
        {
            Teleport();
        }
        
    }
    void Set_BigWter()
    {
        if (a==0)
        {
            Instantiate(Big_Water, Pos, transform.rotation);
            a++;
        }

    }

    void Teleport()
    {
        Skill_SprayWater = false;  //스킬 재 동작 금지
        gameObject.SetActive(false);
        transform.position = new Vector2(Base.position.x, Base.position.y);
        rend.flipX = false;
        Invoke("OnActive", 1f);
        Invoke("Set_BigWter", 1.5f);
    }

    void OnActive()
    {
        gameObject.SetActive(true);
    }
}
