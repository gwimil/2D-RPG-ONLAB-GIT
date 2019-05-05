using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Button m_PlayButton;
    public Button m_SettingsButton;
    public Button m_QuitButton;
    public Button m_BackButton;
    public Button m_PlayerNumber1;
    public Button m_PlayerNumber2;
    public Button m_PlayerNumber3;
    public Text m_ChooseHeroText;

    public Image m_BlackGround;

    public GameObject m_WarriorHero;
    public GameObject m_RangerHero;
    public GameObject m_MageHero;
    private Button m_WarriorButton;
    private Button m_RangerButton;
    private Button m_MageButton;

    
    private int m_PlayerChoosing;

    GameObject go;

    void Start()
    {
        m_PlayButton.gameObject.SetActive(true);
        m_PlayButton.onClick.AddListener(ClickOnPlay);
        m_SettingsButton.gameObject.SetActive(true);
        m_SettingsButton.onClick.AddListener(ClickOnSettings);
        m_QuitButton.gameObject.SetActive(true);
        m_QuitButton.onClick.AddListener(ClickOnQuit);

        m_BackButton.gameObject.SetActive(false);
        m_BackButton.onClick.AddListener(ClickOnBack);

        m_PlayerNumber1.gameObject.SetActive(false);
        m_PlayerNumber1.onClick.AddListener(ClickOnPlayer1Number);
        m_PlayerNumber2.gameObject.SetActive(false);
        m_PlayerNumber2.onClick.AddListener(ClickOnPlayer2Number);
        m_PlayerNumber3.gameObject.SetActive(false);
        m_PlayerNumber3.onClick.AddListener(ClickOnPlayer3Number);

        m_WarriorButton = m_WarriorHero.GetComponentInChildren<Button>();
        m_RangerButton = m_RangerHero.GetComponentInChildren<Button>();
        m_MageButton = m_MageHero.GetComponentInChildren<Button>();

        m_WarriorButton.onClick.AddListener(AddWarrior);
        m_RangerButton.onClick.AddListener(AddRanger);
        m_MageButton.onClick.AddListener(AddMage);

        m_WarriorHero.SetActive(false);
        m_RangerHero.SetActive(false);
        m_MageHero.SetActive(false);
        m_ChooseHeroText.gameObject.SetActive(false);

        m_BlackGround.gameObject.SetActive(false);

        go = new GameObject();
        go = EventSystem.current.currentSelectedGameObject;
    }
    

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null) go = EventSystem.current.currentSelectedGameObject;
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(go);
        }
    }

    void ClickOnPlay()
    {
        turnMainMenu(false);
        m_PlayerNumber1.gameObject.SetActive(true);
        m_PlayerNumber2.gameObject.SetActive(true);
        m_PlayerNumber3.gameObject.SetActive(true);
    }

    void ClickOnSettings()
    {
        turnMainMenu(false);
    }

    void ClickOnQuit()
    {
        Application.Quit();
    }

    void turnMainMenu(bool b)
    {
        m_PlayButton.gameObject.SetActive(b);
        m_SettingsButton.gameObject.SetActive(b);
        m_QuitButton.gameObject.SetActive(b);
        m_BackButton.gameObject.SetActive(!b);
    }

    void ClickOnBack()
    {
        turnMainMenu(true);

        m_BackButton.gameObject.SetActive(false);
        m_PlayerNumber1.gameObject.SetActive(false);
        m_PlayerNumber2.gameObject.SetActive(false);
        m_PlayerNumber3.gameObject.SetActive(false);

        m_ChooseHeroText.gameObject.SetActive(false);
        m_WarriorHero.SetActive(false);
        m_RangerHero.SetActive(false);
        m_MageHero.SetActive(false);
    }

    void ClickOnPlayer1Number()
    {
        MenuData.m_playerNumber = 1;
        MenuData.m_PlayerCharacters[1] = "null";
        MenuData.m_PlayerCharacters[2] = "null";
        PlayersSelected();
    }

    void ClickOnPlayer2Number()
    {
        MenuData.m_playerNumber = 2;
        MenuData.m_PlayerCharacters[2] = "null";
        PlayersSelected();
    }

    void ClickOnPlayer3Number()
    {
        MenuData.m_playerNumber = 3;

        PlayersSelected();

    }

    void PlayersSelected()
    {
        m_PlayerChoosing = 1;
        m_WarriorHero.SetActive(true);
        m_RangerHero.SetActive(true);
        m_MageHero.SetActive(true);
        m_PlayerNumber1.gameObject.SetActive(false);
        m_PlayerNumber2.gameObject.SetActive(false);
        m_PlayerNumber3.gameObject.SetActive(false);
        m_ChooseHeroText.text = "Player "+ m_PlayerChoosing + "choosing";
        m_ChooseHeroText.gameObject.SetActive(true);
    }

    void AddMage()
    {
        switch(m_PlayerChoosing){
            case 1:
                MenuData.m_PlayerCharacters[0] = "Mage";
                break;
            case 2:
                MenuData.m_PlayerCharacters[1] = "Mage";
                break;
            case 3:
                MenuData.m_PlayerCharacters[2] = "Mage";
                break;
            default: break;
        }
        m_PlayerChoosing++;
        m_MageHero.SetActive(false);
        CheckToStartGame();
    }

    void AddRanger()
    {
        switch (m_PlayerChoosing)
        {
            case 1:
                MenuData.m_PlayerCharacters[0] = "Ranger";
                break;
            case 2:
                MenuData.m_PlayerCharacters[1] = "Ranger";
                break;
            case 3:
                MenuData.m_PlayerCharacters[2] = "Ranger";
                break;
            default: break;
        }
        m_PlayerChoosing++;
        m_RangerHero.SetActive(false);
        CheckToStartGame();
    }

    void AddWarrior()
    {

        switch (m_PlayerChoosing)
        {
            case 1:
                MenuData.m_PlayerCharacters[0] = "Warrior";
                break;
            case 2:
                MenuData.m_PlayerCharacters[1] = "Warrior";
                break;
            case 3:
                MenuData.m_PlayerCharacters[2] = "Warrior";
                break;
            default: break;
        }
        m_WarriorHero.SetActive(false);
        m_PlayerChoosing++;

        CheckToStartGame();
    }

    void CheckToStartGame()
    {
        if (m_PlayerChoosing > MenuData.m_playerNumber)
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
            m_BlackGround.gameObject.SetActive(true);
        }
        else
        {
            m_ChooseHeroText.text = "Player " + m_PlayerChoosing + "choosing";
        }
    }

}
