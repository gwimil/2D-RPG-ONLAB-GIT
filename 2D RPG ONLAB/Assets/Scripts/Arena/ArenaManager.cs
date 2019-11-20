using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public Canvas m_MenuCanvas;
        public Button m_BackButton;
        public Button m_MenuButton;
        public Button m_NewCharacter;

    // Start is called before the first frame update
    void Start()
        {
            m_NetworkManager.SetActive(false);
            m_HeroChooser.gameObject.SetActive(true);
            m_MageButton.onClick.AddListener(MageButtonClicked);
            m_RangerButton.onClick.AddListener(RangerButtonClicked);
            m_WarriorButton.onClick.AddListener(WarriorButtonClicked);
            m_BackButton.onClick.AddListener(BackButtonClick);
            m_MenuButton.onClick.AddListener(MenuButtonClick);
            m_NewCharacter.onClick.AddListener(NewCharacterButtonClick);
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


        private void BackButtonClick()
        {
            m_MenuCanvas.gameObject.SetActive(false);
        }
        private void MenuButtonClick()
        {
      Destroy(m_NetworkManager.gameObject);
           MyNetworkManager.Shutdown();
           SceneManager.LoadScene("MENU");
        }

        private void NewCharacterButtonClick()
        {
      Destroy(m_NetworkManager.gameObject);
      MyNetworkManager.Shutdown();
          Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
         }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        if (m_MenuCanvas.gameObject.activeSelf)
        {
          m_MenuCanvas.gameObject.SetActive(false);
        }
        else
        {
          m_MenuCanvas.gameObject.SetActive(true);
        }

      }
    }


  }

}