using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public GameObject Boss;
    public int BossNum;
    CameraShake Shake;
    bool First = true;

    float detailtime;
    float detailtime2;

    //����2
    float time = 0;
    bool SetActive2 = false;
    public ParticleSystem ps;

    void Start()
    {
        Shake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        if(BossNum == 1)
        {
            detailtime = 1;
            detailtime2 = 3;
        }
        if (BossNum == 2)
        {
            detailtime = 3;
            detailtime2 = 6;
        }
        if (BossNum == 3)
        {
            detailtime = 5.5f;
            detailtime2 = 7.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SetActive2)
        {
            if (time < 0.9f)
            {
                Boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 0.9f);
            }
            else
            {
                Invoke("Activefalse", 3f);

            }
            time += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player"&&First)
        {
            First = false;
            //Debug.Log("�ݶ��̴�");
            GameObject.Find("Player").GetComponent<PlayerController>().movable = false;
            if(BossNum <=2)
            {
                Shake.voidShake(3f);
            }
            else
            {
                Shake.voidShake(5f);
            }
            Invoke("detail", detailtime);
            Invoke("Play", detailtime2);

            if ((BossNum == 2))
            {
                //Debug.Log("�¿�Ƽ��");
                Invoke("ColorAlpha", 1f);
            }

            if(BossNum == 3)
            {
                ps.transform.position = Boss.transform.position;
                ps.Play();
                Invoke("Boss3", 5);
            }
        }

    }




    void Play()
    {
        Boss.GetComponent<Enemy>().BossPlay = true;
        GameObject.Find("Player").GetComponent<PlayerController>().movable = true;
    }
    void detail()
    {
        if(BossNum ==1)
        {
            GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("��������").transform.Find("����1").gameObject.SetActive(true);
        }
        if (BossNum == 2)
        {
            GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("��������").transform.Find("����2").gameObject.SetActive(true);
            
        }
        if (BossNum == 3)
        {
            GameObject.Find("Canvas").transform.Find("Ʃ�丮��").transform.Find("��������").transform.Find("����3").gameObject.SetActive(true);
        }
        //1�ʵ� ��ȿ�ϴ� ����
    }

    //

    void ColorAlpha()
    {
        ps.transform.position = Boss.transform.position;
        ps.Play();
        Boss.SetActive(true);
        SetActive2 = true;
    }

    void Boss3()
    {
        Boss.SetActive(true);
    }

}
