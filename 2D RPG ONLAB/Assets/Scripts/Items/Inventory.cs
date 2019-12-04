using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EventCallbacks
{
    public class Inventory : MonoBehaviour
    {
        public bool m_inventoryEnabled;
        public GameObject m_inventory;
        private int m_SlotNumber;
        [HideInInspector] public GameObject[] m_slots;

        public GameObject m_SlotHolder;


        private void Start()
        {
            m_inventory.SetActive(false);
            m_SlotNumber = m_SlotHolder.transform.childCount;
            m_slots = new GameObject[m_SlotNumber];

            for (int i = 0; i < m_SlotNumber; i++)
            {
                m_slots[i] = m_SlotHolder.transform.GetChild(i).gameObject;
            }
        }

        void Update()
        {

            if (m_inventoryEnabled)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    List<Items> sortedItems = new List<Items>();
                    for (int i = 0; i < m_SlotNumber; i++)
                    {
                        Items myItem = m_slots[i].GetComponent<Slot>().m_item;
                        if (myItem != null)
                        {
                            sortedItems.Add(myItem);
                            m_slots[i].GetComponent<Slot>().RemoveAllItemsFromSlot();
                        }
                    }
                    sortedItems.Sort((x, y) => x.m_ID.CompareTo(y.m_ID));
                    for (int i = 0; i < sortedItems.Count; i++)
                    {
                        m_slots[i].GetComponent<Slot>().AddItemToSlot(sortedItems[i]);
                    }
                }

            }

        }

        public Slot GetSlot(int i)
        {
            //Debug.Log(i);
            //Debug.Log(m_SlotNumber);
            return m_slots[i].GetComponent<Slot>();
        }


        public void AddItem(Items item)
        {
            for (int i = 0; i < m_SlotNumber; i++)
            {
                if (m_slots[i].transform.childCount == 2)
                {
                    int hh = item.m_ID;
                    if (m_slots[i].GetComponent<Slot>() == null)
                    {
                        break;
                    }
                    else if (m_slots[i].GetComponent<Slot>().m_item == null)
                    {
                        break;
                    }
                    else
                    {
                        if (item.m_ID == m_slots[i].GetComponent<Slot>().m_item.m_ID)
                        {
                            m_slots[i].GetComponent<Slot>().SumItemsQuantities(item);
                            return;
                        }
                    }
                }
            }

            for (int i = 0; i < m_SlotNumber; i++)
            {
                if (m_slots[i].transform.childCount == 1)
                {
                    m_slots[i].GetComponent<Slot>().AddItemToSlot(item);
                    return;
                }
            }


            // TODO no room in inventory
        }

        public void UseItem(Hero h, int location)
        {
            m_slots[location].GetComponent<Slot>().m_item.Use(h, location);
        }

    }
}
