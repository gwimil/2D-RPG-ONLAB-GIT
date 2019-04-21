using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EventCallbacks
{
    public class Items : MonoBehaviour
    {

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

        virtual public void Use()
        {

        }
        public virtual void Drop()
        {
            // DROPS TO THE GROUND
        }
        public void Sell()
        {
            // sells for the selling price
        }
        public void Buy()
        {
            // if hero has enough money->buy
            //else nothing
        }
    }
}
