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

        public string m_NameOfTeleportEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Hero"))
            {
                if (m_OtherTeleporter)
                {
                    GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Hero");
                    for (int i = 0; i < playersToFind.Length; i++)
                    {
                        playersToFind[i].transform.position = m_TeleporterToTeleportOut.transform.position;
                    }
                }
                else
                {
                    GameObject[] playersToFind = GameObject.FindGameObjectsWithTag("Hero");
                    for (int i = 0; i < playersToFind.Length; i++)
                    {
                        playersToFind[i].transform.position = new Vector3(m_LocationToTeleport.x, m_LocationToTeleport.y, 0);
                    }
                }

                PlaceFoundEventInfo qd = new PlaceFoundEventInfo();
                qd.PlaceName = m_NameOfTeleportEvent;
                EventSystem.Current.FireEvent(qd);

                gameManager.PlayTeleportSound();

            }
        }
    }
}