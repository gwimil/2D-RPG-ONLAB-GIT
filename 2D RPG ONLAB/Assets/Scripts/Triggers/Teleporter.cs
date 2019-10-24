using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class Teleporter : MonoBehaviour
    {
        public float coordX;
        public float coordY;

        public void TeleportTo(Hero hero)
        {
            hero.GetComponent<Rigidbody2D>().position = new Vector2(coordX, coordY);
        }

    }

}