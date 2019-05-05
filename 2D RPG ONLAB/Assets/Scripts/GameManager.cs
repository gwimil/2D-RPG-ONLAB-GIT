
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
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
        public UnityEngine.EventSystems.EventSystem[] m_HeroInventoryEventSystem;

        public GameObject m_ProceduralCave;

        public GameObject CaveTeleporterOut;

        private List<Vector2> placesToSpawn;
        private Vector2 placeToSpawnHeroes;

        private UIManager uiManager;

        private int numberOfPlayers;

        [HideInInspector] public int heroesInCave;


        private System.Guid TeleportEventGuid;

        // Start is called before the first frame update

        private void Awake()
        {
            heroesInCave = 0;
            placesToSpawn = new List<Vector2>();
            m_Camera.m_Targets = new Transform[MenuData.m_playerNumber];
            numberOfPlayers = MenuData.m_playerNumber;
            uiManager = GetComponentInChildren<UIManager>();
        }


        void Start()
        {
            //later the player can choose his hero, sets camera
            CaveTeleporterOut.SetActive(false);
            Debug.Log(MenuData.m_playerNumber);
            UnsetHeroes();
            BindHeroesToPlayers();
            uiManager.changeUI = true;


            for (int i = 0; i < numberOfPlayers; i++)
            {
                m_Players[i].m_hero.GetComponentInChildren<Camera>().gameObject.SetActive(false);
            }


            for (int i = 0; i < 3; i++)
            {
                m_Inventories[i].m_inventory = m_UIInventories[i];
                m_Inventories[i].m_inventory.SetActive(false);
                m_Inventories[i].m_SlotHolder = m_SlotHolders[i];
            }

            EventSystem.Current.RegisterListener<TeleportEventInfo>(TeleportPlayerToNewPlace, ref TeleportEventGuid);

        }
        

        private void BindHeroesToPlayers()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                switch (MenuData.m_PlayerCharacters[i])
                {
                    case "Mage":
                        SetHeroes(i, 2);
                        break;
                    case "Ranger":
                        SetHeroes(i, 1);
                        break;
                    case "Warrior":
                        SetHeroes(i, 0);
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
            m_Players[i].eventSystem = m_HeroInventoryEventSystem[i];
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

        private void TeleportPlayerToNewPlace(TeleportEventInfo tei)
        {
            switch (tei.teleportName)
            {
                case "Cave":
                    TeleportHeroToCave(tei.playerToTeleport.m_PlayerID);
                    Debug.Log("Teleported");
                    break;

                default:
                    break;
            }
        }


        private void TeleportHeroToCave(int id)
        {
            int num;
            if (heroesInCave == 0)
            {
                placesToSpawn = m_ProceduralCave.GetComponent<MapGenerator>().GenerateMapFromManager();

                num = Random.Range(0, placesToSpawn.Count);
                CaveTeleporterOut.transform.position = new Vector3(placesToSpawn[num].x, placesToSpawn[num].y, 0);
                CaveTeleporterOut.SetActive(true);
                num = Random.Range(0, placesToSpawn.Count);
                placeToSpawnHeroes = placesToSpawn[num];
            }
            heroesInCave++;
            
            m_Players[id-1].m_hero.transform.position = new Vector3(placeToSpawnHeroes.x, placeToSpawnHeroes.y, 0);

        }

        public void TeleportPlayerToFixedPosition(string nameOfHero, Vector2 to)
        {
            for (int i = 0; i < m_Heroes.Length; i++)
            {
                if (m_Heroes[i].gameObject.name.Equals(nameOfHero)) m_Heroes[i].transform.position = new Vector3(to.x, to.y, 0);
            }
        }

    }
}
