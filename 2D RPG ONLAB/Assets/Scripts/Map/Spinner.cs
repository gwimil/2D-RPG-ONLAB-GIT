using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    public float m_SpeedToRotate;

    private void Update()
    {
        transform.Rotate(Vector3.forward * m_SpeedToRotate * Time.deltaTime);
    }
}
