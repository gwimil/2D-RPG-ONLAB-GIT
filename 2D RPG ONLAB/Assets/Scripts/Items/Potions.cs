using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    //enum for potion types
    public class Potions : Items
    {
        public string m_effectName;
        public int m_effectDuration;

        override public void Use()
        {
            // make enum for different effect
            // switch case through different effect and add the effect for the hero for the duration
        }
        override public void Drop()
        {
            // drops at the heros location
        }

        public void useEffect()
        {
            //ENUM -> speed, defence, hp, mana potion -> switch case
        }
    }
}
