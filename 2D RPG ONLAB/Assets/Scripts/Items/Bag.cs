using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class Bag : MonoBehaviour
    {
        public Items items;
        public Image image;
        public Image m_lootImage;
        public ItemManager m_ItemManager;
        private int overlappingHeroes;


        private Guid ItemPickedUpGuid;

        private List<Hero> heroesOverlapping;

        private void Awake()
        {
            ItemPickedUpGuid = new Guid();
            heroesOverlapping = new List<Hero>();
        }

        private void Start()
        {
            items = m_ItemManager.ReturnRandomItemsWithMaxLevel(0,20);
            overlappingHeroes = 0;
            image.gameObject.SetActive(false);

            EventSystem.Current.RegisterListener<ItemPickupEventInfo>(OnItemPickedUp, ref ItemPickedUpGuid);

        }

        public void SetItemByMinMax(int min, int max)
        {
            items = m_ItemManager.ReturnRandomItemsWithMaxLevel(min, max);
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
                    heroesOverlapping[i].AddItemToInventory(items);
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
                overlappingHeroes++;
                if (items != null) m_lootImage.sprite = items.m_sprite;
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