using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum for equippabletypes
public class Equippable : Items
{
    public float Quality;
    public float m_plusHP;
    public float m_plusMana;
    public float m_plusArmor;
    public float m_plusmagicResist;
    public float m_plusMeleeDMG;
    public float m_plusArrowDMG;
    public float m_plusMagicDMG;

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
