using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public Hero m_hero;
    public Sprite m_sprite;

    // if quest item use ID-s normally
    // if not quest item: 0-99999 equippable, 100000-199999 potions, 200000-299999 normal items
    [HideInInspector] public int m_ID;
    public bool m_isQuestItem;
    public int m_Quantity;
    public string m_name;
    public string m_description;
    public float m_sellingPrice;
    public float m_buyingPrice;
    

    public virtual void Use()
    {
        // nothing
    }
    public virtual void Drop()
    {
        // DROPS TO THE GROUND
    }
    public  void Sell()
    {
        // sells for the selling price
    }
    public  void Buy()
    {
        // if hero has enough money->buy
        //else nothing
    }

}
