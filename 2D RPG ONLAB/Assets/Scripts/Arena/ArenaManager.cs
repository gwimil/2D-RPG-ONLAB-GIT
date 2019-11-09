﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EventCallbacks
{
    public class ArenaManager : MonoBehaviour
    {

        public GameObject m_NetworkManager;
        public GameObject m_MageHero;
        public GameObject m_RangerHero;
        public GameObject m_WarriorHero;
        public Canvas m_HeroChooser;
        public Button m_MageButton;
        public Button m_RangerButton;
        public Button m_WarriorButton;

        // Start is called before the first frame update
        void Start()
        {
            m_NetworkManager.SetActive(false);
            m_HeroChooser.gameObject.SetActive(true);
            m_MageButton.onClick.AddListener(MageButtonClicked);
            m_RangerButton.onClick.AddListener(RangerButtonClicked);
            m_WarriorButton.onClick.AddListener(WarriorButtonClicked);
        }


        private void MageButtonClicked()
        {
            m_HeroChooser.gameObject.SetActive(false);
            m_NetworkManager.SetActive(true);
            m_NetworkManager.GetComponent<MyNetworkManager>().chosenCharacter = 0;
    
    }

        private void RangerButtonClicked()
        {
            m_HeroChooser.gameObject.SetActive(false);
            m_NetworkManager.SetActive(true);
            m_NetworkManager.GetComponent<MyNetworkManager>().chosenCharacter = 1;
        }

        private void WarriorButtonClicked()
        {
            m_HeroChooser.gameObject.SetActive(false);
            m_NetworkManager.SetActive(true);
            m_NetworkManager.GetComponent<MyNetworkManager>().chosenCharacter = 2;
        }


    }

}