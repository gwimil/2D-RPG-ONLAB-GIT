using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ItemManager m_ItemManager;
    public QuestManager m_QuestManager;
    public Player2D[] m_Players;
    public Hero[] m_Heroes;
    public Inventory[] m_Inventories;
    public GameObject[] m_UIInventories;
    public GameObject[] m_SlotHolders;
    public CameraControll m_Camera;

   


    // Start is called before the first frame update
    void Start()
    {
        //later the player can choose his hero, sets camera
        m_Camera.m_Targets = new Transform[MenuData.m_playerNumber];

        UnsetHeroes();
        BindHeroesToPlayers();


        
        
        for (int i = 0; i < 3; i++)
        {
            m_Inventories[i].m_inventory = m_UIInventories[i];
            m_Inventories[i].m_SlotHolder = m_SlotHolders[i];
        }

    }
    

    private void BindHeroesToPlayers()
    {
        for (int i = 0; i < MenuData.m_playerNumber; i++)
        {
            switch (MenuData.m_PlayerCharacters[i])
            {
                case "Mage":
                    SetHeroes(i,2);
                    break;
                case "Ranger":
                    SetHeroes(i,1);
                    break;
                case "Warrior":
                    SetHeroes(i,0);
                    break;
                default: break;
            }
        }
        
    }

    private void SetHeroes(int i, int heronumber)
    {
        m_Players[i].m_hero = m_Heroes[heronumber];
        m_Players[i].m_hero.transform.parent = m_Players[i].transform;
        m_Players[i].gameObject.SetActive(true);
        m_Players[i].m_hero.gameObject.SetActive(true);
        m_Camera.m_Targets[i] = m_Players[i].m_hero.transform;
    }

    private void UnsetHeroes()
    {
        for (int i = 0; i < 3; i++)
        {
           m_Players[i].gameObject.SetActive(false);
           m_Heroes[i].gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Items item = m_ItemManager.GiveItem(1);
            item.m_Quantity = 1;
            m_Players[0].m_hero.AddItemToInventory(item);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Items item = m_ItemManager.GiveItem(0);
            item.m_Quantity = 1;
            m_Players[0].m_hero.AddItemToInventory(item);
        }
    }
}
