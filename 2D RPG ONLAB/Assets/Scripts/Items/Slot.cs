using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector] public Items m_item;
    [HideInInspector] public int m_ID;
    private Image nullImage;
    private string m_Quantity;


    private void Start()
    {
        GetComponentInChildren<Text>().text = "";
        nullImage = GetComponent<Image>();
        m_item = null;
    }

    public void AddItemToSlot(Items item)
    {
        m_item = Instantiate(item, this.transform);
        m_item.name = m_item.m_name;
        GetComponentInChildren<Text>().text = m_item.m_Quantity.ToString();
        GetComponent<Image>().sprite = item.m_sprite;
    }

    public void RemoveItemFromSlot()
    {
        GetComponentInChildren<Text>().text = "";
        GetComponent<Image>().sprite = nullImage.sprite;
        Destroy(m_item.gameObject);
        m_item = null;
    }

    public void SumItemsQuantities(Items item)
    {
        m_item.m_Quantity = m_item.m_Quantity +  item.m_Quantity;
        GetComponentInChildren<Text>().text = m_item.m_Quantity.ToString();
    }


}
