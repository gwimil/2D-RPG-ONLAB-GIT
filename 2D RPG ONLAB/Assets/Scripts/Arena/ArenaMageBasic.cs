using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class ArenaMageBasic : ArenaProjectiles
    {
        public Sprite m_Sprite;

        private void Start()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = m_Sprite;
        }
    }

}