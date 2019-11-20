
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class GameManager : MonoBehaviour
    {

        public AudioClip normalBackground;
        public AudioClip caveBackground;
        public AudioClip bossBackground;
        public AudioClip teleportSound;


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

        public GameObject m_TeleportedToTheCave;
        public GameObject m_CaveTeleporterOut;
        public GameObject m_TeleportedInCave;
        public GameObject m_TeleportToFirstBoss;
        public GameObject m_TeleporterToSecondBoss;

        public GameObject m_BagToPutInCave;


        private List<Vector2> placesToSpawn;
        private Vector2 placeToSpawnHeroes;

        private UIManager uiManager;

        private int numberOfPlayers;

        private System.Guid TeleportEventGuid;

        // Start is called before the first frame update

        private void Awake()
        {
            placesToSpawn = new List<Vector2>();
            m_Camera.m_Targets = new Transform[MenuData.m_playerNumber];
            numberOfPlayers = MenuData.m_playerNumber;
            uiManager = GetComponentInChildren<UIManager>();
        }


        void Start()
        {
            //later the player can choose his hero, sets camera
            m_CaveTeleporterOut.SetActive(false);
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

            SetUpCave();

            EventSystem.Current.RegisterListener<PlaceFoundEventInfo>(TeleportPlayerToNewPlace, ref TeleportEventGuid);
            EventSystem.Current.RegisterListener<MobQuestDoneEventInfo>(EnemyQuestDone, ref TeleportEventGuid);

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

        }

        private void TeleportPlayerToNewPlace(PlaceFoundEventInfo tei)
        {
            switch (tei.PlaceName)
            {
                case "outCave":
                    m_TeleportToFirstBoss.SetActive(true);
                    SetUpCave();
                    StartNormalSound();
                    break;
                case "cave":
                    StartCaveSound();
                    break;
                default:
                    break;
            }
        }

        private void StartCaveSound()
        {
            m_Camera.GetComponentInChildren<AudioSource>().clip = caveBackground;
            m_Camera.GetComponentInChildren<AudioSource>().Play();
        }

        private void StartNormalSound()
        {
            m_Camera.GetComponentInChildren<AudioSource>().clip = normalBackground;
            m_Camera.GetComponentInChildren<AudioSource>().Play();
        }


        private void EnemyQuestDone(MobQuestDoneEventInfo mqe)
        {
            switch (mqe.MobName)
            {
                case "dummy":
                    m_TeleportedToTheCave.SetActive(true);
                    break;
                case "mage":
                    m_TeleporterToSecondBoss.SetActive(true);
                    break;

                default:
                    break;
            }
        }


        public void PlayTeleportSound()
        {
            gameObject.GetComponent<AudioSource>().clip = teleportSound;
            gameObject.GetComponent<AudioSource>().Play();
        }

          private void SetUpCave()
          {
              int num;
              placesToSpawn = m_ProceduralCave.GetComponent<MapGenerator>().GenerateMapFromManager();

              num = Random.Range(0, placesToSpawn.Count);
              m_CaveTeleporterOut.transform.position = new Vector3(placesToSpawn[num].x, placesToSpawn[num].y, 0);
              m_CaveTeleporterOut.SetActive(true);
              num = Random.Range(0, placesToSpawn.Count);
              placeToSpawnHeroes = placesToSpawn[num];
              m_TeleportedInCave.transform.position = new Vector3(placeToSpawnHeroes.x, placeToSpawnHeroes.y, 0);

            for (int i = 0; i < 5; i++)
            {
                num = Random.Range(0, placesToSpawn.Count);
                GameObject b = Instantiate(m_BagToPutInCave, new Vector3(placesToSpawn[num].x, placesToSpawn[num].y, 0), Quaternion.Euler(0,0,0));
                b.GetComponent<Bag>().SetItemByMinMax(5, 10);
            }

          }




    }
}