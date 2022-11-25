using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    Quaternion m_originRot;

    private void Start()
    {
        m_originRot = transform.rotation;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(ShakeCoroution());
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            StopCoroutine(ShakeCoroution());
            StartCoroutine(Reset());
            StopAllCoroutines();
        }
    }

    IEnumerator ShakeCoroution()
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

    IEnumerator Reset()
    {
        while (Quaternion.Angle(transform.rotation, m_originRot) > 0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        }
    }

}