using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks {
    public class SimpleTeleporter : MonoBehaviour
    {
        public GameManager gameManager;
        public Vector2 m_LocationToTeleport;

        public bool m_OtherTeleporter;
        public GameObject m_TeleporterToTeleportOut;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Hero"))
            {
                Debug.Log("Colliedddd");
                Debug.Log(m_OtherTeleporter);
                if (m_OtherTeleporter)
                {
                    collision.gameObject.GetComponent<Hero>().transform.position = m_TeleporterToTeleportOut.transform.position;
                }
                else
                {
                    collision.gameObject.GetComponent<Hero>().transform.position = new Vector3(m_LocationToTeleport.x, m_LocationToTeleport.y, 0);
                }
                gameManager.heroesInCave--;
            }
        }


    }
}
