using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class CameraManager : MonoBehaviour
    {
        private Camera mainCamera;
        private CameraControll cameraControll;
        private bool usingMainCamera;
        public Player2D[] players;

        public Canvas canvas;

        public RawImage m_Image;

        public float m_SizeBeforeDeviding;
        public float m_SizeBeforeForging;

        private int numberOfPlayers;

        private void Awake()
        {
            m_Image.gameObject.SetActive(false);
            usingMainCamera = true;
            mainCamera = GetComponentInChildren<Camera>();
            cameraControll = GetComponent<CameraControll>();
            numberOfPlayers = MenuData.m_playerNumber;
        }
        
        private void FixedUpdate()
        {
            cameraControll.FindAveragePosition();
            if (usingMainCamera)
            {
                if (cameraControll.FindRequiredSize() >= m_SizeBeforeDeviding)
                {
                    m_Image.rectTransform.sizeDelta = new Vector2(0.3f, Screen.height);
                    m_Image.gameObject.SetActive(true);

                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        players[i].m_hero.m_HeroCamera.gameObject.SetActive(true);
                    }

                    if (numberOfPlayers == 2)
                    {
                        if (players[0].m_hero.transform.position.x < players[1].m_hero.transform.position.x)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(0,0,0.5f,1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(0.5f, 0, 1, 1);
                        }
                        else
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(0.5f, 0, 1, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(0, 0, 0.5f, 1);
                        }
                    }
                    if (numberOfPlayers == 3)
                    {
                        //TODO
                    }


                    mainCamera.gameObject.SetActive(false);
                    usingMainCamera = false;
                }
            }
            else
            {
                if (cameraControll.FindRequiredSize() < m_SizeBeforeForging)
                {
                    m_Image.gameObject.SetActive(false);
                    mainCamera.gameObject.SetActive(true);

                    for(int i = 0; i < numberOfPlayers; i++)
                    {
                        players[i].m_hero.GetComponentInChildren<Camera>().gameObject.SetActive(false);
                    }
                    usingMainCamera = true;
                }
            }
            
        }
    }
    
}

