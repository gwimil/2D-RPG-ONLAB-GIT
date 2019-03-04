using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void AddItem(Items item)
    {

        for (int i = 0; i < m_allSlots; i++)
        {
            if (m_slots[i].transform.childCount == 0)
            {
                m_slots[i].GetComponent<Slot>().AddItemToSlot(item);
                return;
            }
            else if (item.m_ID == m_slots[i].GetComponent<Slot>().m_item.m_ID)
            {
                m_slots[i].GetComponent<Slot>().SumItemsQuantities(item);
                return;
            }
        }

        // no room in inventory
    }
        

}
