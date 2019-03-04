using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector] public Items m_item;
    [HideInInspector] public int m_ID;
    private Image image;
    


    private void Start()
    {

        image = GetComponent<Image>();   
    }

    public void AddItemToSlot(Items item)
    {
        m_item = Instantiate(item, this.transform);
        GetComponent<Image>().sprite = item.m_sprite;
    }

    public void SumItemsQuantities(Items item)
    {
        Debug.Log("WHY ARE YOU GAY");
        Debug.Log(m_item.m_Quantity + " + " + item.m_Quantity + " = " + (m_item.m_Quantity + item.m_Quantity));
        m_item.m_Quantity = m_item.m_Quantity +  item.m_Quantity;
    }
    

}
