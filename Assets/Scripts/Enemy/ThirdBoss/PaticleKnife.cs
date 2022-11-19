using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleKnife : MonoBehaviour
{
    public GameObject paticle;
    GameObject targetGameObject;

    public Transform PlayerPos;
    float AddTime = 4;  //�߰� ������ ���󰡴� �ð�
    float sp = 200;
    Vector2 direction;
    bool tr = true;
    Vector3 lookDir;

    Transform Reset;

    void Start()
    {
        Invoke("OnDestroy", 7);
        Reset = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (AddTime >= 1)   //�߰� �Լ�
        {
            Vector3 dir = PlayerPos.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

           
        }
        if (AddTime <= 1)
        {
            if(tr)
            {
                direction = (PlayerPos.position - transform.position).normalized;  //������ �߰�pos ��
                tr = false; 
            }
            
        }
        if (AddTime <= 0)
        {
            //transform.Translate(direction * sp * Time.deltaTime);
            transform.position = Vector3.MoveTowards(Reset.position, direction, sp * Time.deltaTime);
            Instantiate(paticle, transform.position,transform.rotation);
        }
        AddTime -= Time.deltaTime;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
