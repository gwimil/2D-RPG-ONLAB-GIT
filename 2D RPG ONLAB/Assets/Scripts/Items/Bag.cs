using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class Bag : MonoBehaviour
    {
        [HideInInspector] public List<Items> items;
        public Image image;
        public Image m_lootImage;
        public ItemManager m_ItemManager;
        private int overlappingHeroes;


        private Guid ItemPickedUpGuid;

        private List<Hero> heroesOverlapping;

        private void Awake()
        {
            ItemPickedUpGuid = new Guid();
            items = new List<Items>();
            heroesOverlapping = new List<Hero>();
        }

        private void Start()
        {
            items.Add(m_ItemManager.ReturnRandomItemsWithMaxLevel(0,20));
            overlappingHeroes = 0;
            image.gameObject.SetActive(false);

            EventSystem.Current.RegisterListener<ItemPickupEventInfo>(OnItemPickedUp, ref ItemPickedUpGuid);

        }

        public void SetItemByMinMax(int min, int max)
        {
            items.Clear();
            items.Add(m_ItemManager.ReturnRandomItemsWithMaxLevel(min, max));
        }

        private void OnItemPickedUp(ItemPickupEventInfo ui)
        {
            switch (ui.HeroName)
            {
                case "Mage": GiveItemToHero("Mage");
                    break;
                case "Ranger":
                    GiveItemToHero("Ranger");
                    break;
                case "Warrior":
                    GiveItemToHero("Warrior");
                    break;
            }
        }

        private void GiveItemToHero(string heroName)
        {
            for (int i = 0; i <heroesOverlapping.Count; i++)
            {
                if (heroesOverlapping[i].gameObject.name == heroName)
                {
                    heroesOverlapping[i].AddItemToInventory(m_ItemManager.GiveItem(items[0].m_ID));
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Hero")
            {
                collision.GetComponent<Hero>().CollideWithBag(true);
                heroesOverlapping.Add(collision.GetComponent<Hero>());
                Debug.Log(items.Count);
                overlappingHeroes++;
                if (items.Count >= 1) m_lootImage.sprite = items[0].m_sprite;
                image.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            overlappingHeroes--;
            if (collision.tag == "Hero" && overlappingHeroes <=0)
            {
                collision.GetComponent<Hero>().CollideWithBag(false);
                heroesOverlapping.Remove(collision.GetComponent<Hero>());
                image.gameObject.SetActive(false);
            }
        }



    }
}