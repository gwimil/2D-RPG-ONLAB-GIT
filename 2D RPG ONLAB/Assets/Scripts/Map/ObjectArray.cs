using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArray : MonoBehaviour
{
    public GameObject m_SelfObject;
    public int m_NumberOfObjects;
    public float m_Distance;
    public bool m_Horizontal;


    private List<GameObject> listOfObjects;

    private float positionX;
    private float positionY;

    private void Awake()
    {
        listOfObjects = new List<GameObject>();
        positionX = 0.0f;
        positionY = 0.0f;

    }

    private void Start()
    {
        for (int i = 0; i < m_NumberOfObjects; i++)
        {
            if (m_Horizontal) positionX += m_Distance;
            else positionY += m_Distance;
            Vector3 spawnTO = new Vector3(positionX, positionY, 0);
            listOfObjects.Add(Instantiate(m_SelfObject, transform.position + spawnTO, Quaternion.Euler(0, 0, 0), this.transform));
            listOfObjects[i].gameObject.name = m_SelfObject.gameObject.name;
        }
    }

}
