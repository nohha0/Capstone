using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    Quaternion m_originRot;

    PlayerController Shake;

    private void Start()
    {
        m_originRot = transform.rotation;
        Shake = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    private void Update()
    {
        if (Shake.CameraController)
        {
            StartCoroutine(ShakeCoroution());
            Shake.CameraController = false;
            Invoke("Stopcoroutin", 0.8f);

        }



    }

    public IEnumerator ShakeCoroution()
    {
        Vector3 t_orginEuler = transform.eulerAngles;
        while (true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_royZ = Random.Range(-m_offset.z, m_offset.z);

            Vector3 t_randomRot = t_orginEuler + new Vector3(t_rotX, t_rotY, t_royZ);
            Quaternion t_rot = Quaternion.Euler(t_randomRot);

            while (Quaternion.Angle(transform.rotation, t_rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }

    }
    public void voidShake(float a)
    {
        StartCoroutine(ShakeCoroution());
        Shake.CameraController = false;
        Invoke("Stopcoroutin", a);
    }

    void Stopcoroutin()
    {
        StopCoroutine(ShakeCoroution());
        StartCoroutine(Reset());
        StopAllCoroutines();
    }


    public IEnumerator Reset()
    {
        while (Quaternion.Angle(transform.rotation, m_originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }
}