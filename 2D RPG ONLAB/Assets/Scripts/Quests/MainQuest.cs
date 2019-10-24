using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{

    public class MainQuest : MonoBehaviour
    {
        public Items[] m_Rewards;
        public string m_Description;
        public int m_QuestID;
        public int m_Exp;
        public int m_SuggestedLevel;

        public string GetDestription()
        {
            return m_Description;
        }

        public Items[] GetItems()
        {
            return m_Rewards;
        }



    }
}
