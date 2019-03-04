using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Items> m_EquippableItems;
    public List<Items> m_Potions;
    public List<Items> m_NormalItems;


    // Start is called before the first frame update
    void Start()
    {
        m_EquippableItems = new List<Items>();
        for (int i = 1; i < m_EquippableItems.Count; i++)
        {
            m_EquippableItems[i].m_ID = i;
        }

        m_Potions = new List<Items>();
        for (int i = 1; i < m_Potions.Count; i++)
        {
            m_Potions[i].m_ID = i + 100000;
        }

        m_NormalItems = new List<Items>();
        for (int i = 1; i < m_NormalItems.Count; i++)
        {
            m_NormalItems[i].m_ID = i + 2000000;
        }
    }
    
    public List<Items> DropItemsFromEnemies()
    {
        List<Items> items = new List<Items>();
        // drops a list of items from enemies
        // different enemies-> different Items ->enum/tags -> switch-case
        return items;
    }

    public Items GiveItem(int i)
    {
        if (i < 100001) return m_EquippableItems[i];
        else if (i < 200000) return m_Potions[i];
        else return m_NormalItems[i];
    } 



}
