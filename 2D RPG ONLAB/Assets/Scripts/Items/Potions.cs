using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    //enum for potion types
    public class Potions : Items
    {
        public IPotions m_Potion_Effect;
        public int m_EffectDuration;
        public int m_EffectStrength;

        override public void Use(Hero h)
        {
            switch (m_Potion_Effect)
            {
                case IPotions.Speed:
                    h.currentBuff = m_Potion_Effect;
                    h.buffDuration = m_EffectDuration * 1000;
                    h.buffStrength = m_EffectStrength;
                    break;
                case IPotions.InstantHealth:
                    h.HealHero(m_EffectStrength);
                    break;
                case IPotions.InstantMana:
                    h.ManaHero(m_EffectStrength);
                    break;
                default: break;
            }
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
