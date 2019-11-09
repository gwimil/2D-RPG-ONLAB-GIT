using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EventCallbacks
{
    public class Slot : MonoBehaviour
    {
        [HideInInspector] public Items m_item;
        [HideInInspector] public int m_ID;
        private Sprite nullSprite;
        private string m_Quantity;


        private void Start()
        { 
            if (gameObject.GetComponentInChildren<Text>() != null)
            {
                GetComponentInChildren<Text>().text = "";
            }
            nullSprite = null;
        }

        public void AddItemToSlot(Items item, bool b = true)
        {
            m_item = Instantiate(item, this.transform);
            m_item.gameObject.name = item.gameObject.name;
            if (b) GetComponentInChildren<Text>().text = m_item.m_Quantity.ToString();
            GetComponent<Image>().sprite = item.m_sprite;
        }

        public void RemoveItemFromSlot(bool b = true)
        {
            if (m_item.m_Quantity > 1)
            {
                m_item.m_Quantity--;
                GetComponentInChildren<Text>().text = m_item.m_Quantity.ToString();
            }
            else
            {
                if (b) GetComponentInChildren<Text>().text = "";
                GetComponent<Image>().sprite = nullSprite;
                Destroy(m_item.gameObject);
                m_item = null;
            }
        }

        public void RemoveAllItemsFromSlot(bool b = true)
        {
            if (b) GetComponentInChildren<Text>().text = "";
            GetComponent<Image>().sprite = nullSprite;
            Destroy(m_item.gameObject);
            m_item = null;
        }

        public void AddToEmptyEqupmentSlot(Slot s)
        {
            AddItemToSlot(s.m_item, true);
            s.RemoveItemFromSlot();
        }

        public void SumItemsQuantities(Items item)
        {
            m_item.m_Quantity = m_item.m_Quantity + item.m_Quantity;
            GetComponentInChildren<Text>().text = m_item.m_Quantity.ToString();
        }

    }
}
