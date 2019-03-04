using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    bool m_inventoryEnabled;
    public GameObject m_inventory;

    private int m_allSlots;
    private GameObject[] m_slots;

    public GameObject m_SlotHolder;



    private void Start()
    {
        m_allSlots = 25;
        m_slots = new GameObject[m_allSlots];

        for (int i = 0; i < m_allSlots; i++)
        {
            m_slots[i] = m_SlotHolder.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            m_inventoryEnabled = !m_inventoryEnabled;
        }

        if (m_inventoryEnabled) m_inventory.SetActive(true);
        else m_inventory.SetActive(false);
    }

    public void AddItem(Items i)
    {
        // adds the item to the slot
    }
        

}
