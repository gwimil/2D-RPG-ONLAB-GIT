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
        for (int i = 0; i < m_EquippableItems.Count; i++)
        {
            m_EquippableItems[i].m_ID = i;
        }
        
        for (int i = 0; i < m_Potions.Count; i++)
        {
            m_Potions[i].m_ID = i + 100000;
        }
        
        for (int i = 0; i < m_NormalItems.Count; i++)
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
        if (i < 100000) return m_EquippableItems[i];
        else if (i < 200000) return m_Potions[i];
        else return m_NormalItems[i];
    } 



}
