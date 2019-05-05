using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class TeleportTrigger : MonoBehaviour
    {
        public GameManager gameManager;
        public Vector2 m_PositionToTeleport;
        public Text m_UseText;

        public bool m_WaitForResponse;
        public string m_NameOfPlaceToTeleport;

        private int numberOfHeroes;

        private void Awake()
        {
            numberOfHeroes = 0;
            m_UseText.gameObject.SetActive(false);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Hero"))
            {
                if (m_WaitForResponse)
                {
                    numberOfHeroes++;
                    m_UseText.gameObject.SetActive(true);
                    collision.gameObject.GetComponent<Hero>().overTeleport = true;
                    collision.gameObject.GetComponent<Hero>().teleportName = m_NameOfPlaceToTeleport;
                }
                else
                {
                    gameManager.TeleportPlayerToFixedPosition(collision.gameObject.name, m_PositionToTeleport);
                }
               
                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Hero"))
            {
                collision.gameObject.GetComponent<Hero>().overTeleport = false;
                collision.gameObject.GetComponent<Hero>().teleportName = "";
                numberOfHeroes--;
                if (numberOfHeroes <= 0) m_UseText.gameObject.SetActive(false);
            }
        }


    }
}

