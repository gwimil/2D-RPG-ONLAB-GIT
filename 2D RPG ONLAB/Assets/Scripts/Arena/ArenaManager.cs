using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    [HideInInspector] public GameObject choosenHero;



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
        choosenHero = m_MageHero;
        m_HeroChooser.gameObject.SetActive(false);
        m_NetworkManager.SetActive(true);
    }

    private void RangerButtonClicked()
    {
        choosenHero = m_RangerHero;
        m_HeroChooser.gameObject.SetActive(false);
        m_NetworkManager.SetActive(true);

    }

    private void WarriorButtonClicked()
    {
        choosenHero = m_WarriorHero;
        m_HeroChooser.gameObject.SetActive(false);
        m_NetworkManager.SetActive(true);
    }


}
