using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    //enum for equippabletypes
    public class Equippable : Items
    {
        public float Quality;
        public float m_plusHP;
        public float m_plusMana;
        public float m_plusArmor;
        public float m_plusmagicResist;
        public float m_plusDMG;


        override public void UpdateStatsWithItem(Hero h, bool add)
        {
            if (add)
            {
                h.m_Armor += m_plusArmor;
                h.m_BaseDMG += m_plusDMG;
                h.m_MaxHP += m_plusHP;
                h.m_MaxMana += m_plusMana;
                h.m_MagicResist += m_plusmagicResist;
            }
            else
            {
                h.m_Armor -= m_plusArmor;
                h.m_BaseDMG -= m_plusDMG;
                h.m_MaxHP -= m_plusHP;
                h.m_MaxMana -= m_plusMana;
                h.m_MagicResist -= m_plusmagicResist;
            }
            
        }

        override public void Use()
        {

            //enum for armor types -> switch case
            // equip the armor
            //if alreadyequipped -> change slot
            //if no armor equipped -> equip



        }
        override public void Drop()
        {
            // drops the armor to the players location
        }

    }
}