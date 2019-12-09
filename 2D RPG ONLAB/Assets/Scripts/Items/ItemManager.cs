using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
  public class ItemManager : MonoBehaviour
  {
    public List<Items> m_EquippableItems;
    public List<Items> m_Potions;
    public List<Items> m_NormalItems;




    public List<Items> DropItemsFromEnemies(int numberOfItems = -1)
    {
      List<Items> items = new List<Items>();
      // drops a list of items from enemies
      // different enemies-> different Items ->enum/tags -> switch-case
      if (numberOfItems == -1)
      {
        items.Add(m_EquippableItems[0]);
      }
      else
      {
        for (int i = 0; i < numberOfItems; i++)
        {
          items.Add(m_EquippableItems[0]);
        }
      }

      return items;
    }


    public Items ReturnRandomItemsWithMaxLevel(int minLevel, int maxLevel)
    {
      Items itemDrop = null;
      float randomNumber = Random.Range(0.0f, 1.0f);
      if (randomNumber <= 0.2)
      {
        int rand = Random.Range(0, m_Potions.Count);
        itemDrop = m_Potions[rand];
      }
      else
      {
        bool foundItem = false;
        while (!foundItem)
        {
          int rand = Random.Range(0, m_EquippableItems.Count);
          float quality = m_EquippableItems[rand].gameObject.GetComponent<Equippable>().Quality;
          int qual = (int)quality;
          if (qual <= maxLevel && qual >= minLevel)
          {
            itemDrop = m_EquippableItems[rand];
            foundItem = true;
          }

        }
      }
      return itemDrop;
    }


  }
}