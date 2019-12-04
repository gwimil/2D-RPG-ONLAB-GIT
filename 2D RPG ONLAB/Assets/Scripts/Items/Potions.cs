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

        override public void Use(Hero h, int location)
        {
            switch (m_Potion_Effect)
            {
                case IPotions.Speed:
                    h.PotionDrinked(IPotions.Speed, m_EffectStrength, m_EffectDuration, location);
                    break;
                case IPotions.InstantHealth:
                    h.PotionDrinked(IPotions.InstantHealth, m_EffectStrength, m_EffectDuration, location);
                    break;
                case IPotions.InstantMana:
                    h.PotionDrinked(IPotions.InstantMana, m_EffectStrength, m_EffectDuration, location);
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
