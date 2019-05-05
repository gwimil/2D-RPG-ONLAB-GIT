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

        public UIManager m_UIManager;
        public Player2D[] players;
        public RawImage m_ImageMiddle;
        public RawImage m_ImageLeft;
        public RawImage m_ImageRight;
        public float m_SizeBeforeDeviding;
        public float m_SizeBeforeForging;



        private int numberOfPlayers;

        private bool positionsChanged;

        private float p1Prev;
        private float p2Prev;
        private float p3Prev;

        private float player1CurrPos;
        private float player2CurrPos;
        private float player3CurrPos;

        private bool first;

        private void Awake()
        {
            first = true;
            positionsChanged = false;
            m_ImageMiddle.gameObject.SetActive(false);
            m_ImageLeft.gameObject.SetActive(false);
            m_ImageRight.gameObject.SetActive(false);
            usingMainCamera = true;
            mainCamera = GetComponentInChildren<Camera>();
            cameraControll = GetComponent<CameraControll>();
            numberOfPlayers = MenuData.m_playerNumber;
        }

        private void Update()
        {
            if (first)
            {
                if (numberOfPlayers >= 1)
                    p1Prev = players[0].m_hero.transform.position.x;
                if (numberOfPlayers >= 2)
                    p2Prev = players[1].m_hero.transform.position.x;
                if (numberOfPlayers >= 3)
                    p2Prev = players[2].m_hero.transform.position.x;
                first = false;
            }
            
            player1CurrPos = players[0].m_hero.transform.position.x;
            if (numberOfPlayers >= 2) player2CurrPos = players[1].m_hero.transform.position.x;
            if (numberOfPlayers >= 3) player3CurrPos = players[2].m_hero.transform.position.x;

            if (numberOfPlayers == 2)
            {
                if (p1Prev <= p2Prev && player1CurrPos > player2CurrPos || p2Prev <= p1Prev && player2CurrPos > player1CurrPos)
                {
                    positionsChanged = true;
                }
            }
            if (numberOfPlayers == 3)
            {
                if ((p1Prev <= p2Prev && player1CurrPos > player2CurrPos) ||(p1Prev <= p3Prev && player1CurrPos > player3CurrPos) ||(p2Prev <= p3Prev && player2CurrPos > player3CurrPos)||
                    (p2Prev <= p1Prev && player2CurrPos > player1CurrPos) || (p3Prev <= p1Prev && player3CurrPos > player1CurrPos) || (p3Prev <= p2Prev && player3CurrPos > player2CurrPos))
                {
                    positionsChanged = true;
                }
            }

            p1Prev = player1CurrPos;
            if (numberOfPlayers >= 2) p2Prev = player2CurrPos;
            if (numberOfPlayers >= 3) p3Prev = player3CurrPos;

        }

        private void FixedUpdate()
        {
            cameraControll.FindAveragePosition();
            if (usingMainCamera || positionsChanged)
            {
                if (cameraControll.FindRequiredSize() >= m_SizeBeforeDeviding)
                {
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        players[i].m_hero.m_HeroCamera.gameObject.SetActive(true);
                    }

                    if (numberOfPlayers == 2)
                    {
                        m_ImageMiddle.rectTransform.sizeDelta = new Vector2(0.3f, Screen.height);
                        m_ImageMiddle.gameObject.SetActive(true);

                        if (player1CurrPos < player2CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(0,0,0.5f,1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
                        }
                        else
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(0.5f, 0, 0.5f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(0, 0, 0.5f, 1);
                        }
                    }
                    else if (numberOfPlayers == 3)
                    {
                        m_ImageLeft.rectTransform.sizeDelta = new Vector2(0.3f, Screen.height);
                        m_ImageRight.rectTransform.sizeDelta = new Vector2(0.3f, Screen.height);
                        m_ImageLeft.rectTransform.anchoredPosition = new Vector2(- Screen.width / 6, 0);
                        m_ImageRight.rectTransform.anchoredPosition = new Vector2(Screen.width / 6, 0);
                        m_ImageLeft.gameObject.SetActive(true);
                        m_ImageRight.gameObject.SetActive(true);


                        if (player1CurrPos < player2CurrPos && player2CurrPos < player3CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(0, 0, 1/3f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(1 / 3f, 0, 1 / 3f, 1);
                            players[2].m_hero.m_HeroCamera.rect = new Rect(2 / 3f, 0, 1 / 3f, 1);
                        }
                        else if (player1CurrPos < player3CurrPos && player3CurrPos < player2CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(0, 0, 1 / 3f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(2 / 3f, 0, 1 / 3f, 1);
                            players[2].m_hero.m_HeroCamera.rect = new Rect(1 / 3f, 0, 1 / 3f, 1);
                        }
                        else if (player3CurrPos < player1CurrPos && player1CurrPos < player2CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(1 / 3f, 0, 1 / 3f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(2 / 3f, 0, 1 / 3f, 1);
                            players[2].m_hero.m_HeroCamera.rect = new Rect(0, 0, 1 / 3f, 1);
                        }
                        else if (player3CurrPos < player2CurrPos && player2CurrPos < player1CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(2 / 3f, 0, 1 / 3f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(1 / 3f, 0, 1 / 3f, 1);
                            players[2].m_hero.m_HeroCamera.rect = new Rect(0, 0, 1 / 3f, 1);
                        }
                        else if (player2CurrPos < player3CurrPos && player3CurrPos < player1CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(2 / 3f, 0, 1 / 3f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(0, 0, 1 / 3f, 1);
                            players[2].m_hero.m_HeroCamera.rect = new Rect(1 / 3f, 0, 1 / 3f, 1);
                        }
                        else if (player2CurrPos < player1CurrPos && player1CurrPos < player3CurrPos)
                        {
                            players[0].m_hero.m_HeroCamera.rect = new Rect(1 / 3f, 0, 1 / 3f, 1);
                            players[1].m_hero.m_HeroCamera.rect = new Rect(0, 0, 1 / 3f, 1);
                            players[2].m_hero.m_HeroCamera.rect = new Rect(2 / 3f, 0, 1 / 3f, 1);
                        }
                    }


                    mainCamera.gameObject.SetActive(false);
                    usingMainCamera = false;
                }
                positionsChanged = false;
                m_UIManager.changeUI = true;
            }
            else
            {
                if (cameraControll.FindRequiredSize() < m_SizeBeforeForging)
                {
                    m_ImageMiddle.gameObject.SetActive(false);
                    m_ImageLeft.gameObject.SetActive(false);
                    m_ImageRight.gameObject.SetActive(false);
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

