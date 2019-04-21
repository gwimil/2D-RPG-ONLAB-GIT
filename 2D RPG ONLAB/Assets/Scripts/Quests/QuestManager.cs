using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class QuestManager : MonoBehaviour
    {
        public static Quest m_CurrentQuest;
        public List<Quest> m_Quests;

        public Mobs[] m_MobsToKillInQuests;
        public Items[] m_ItemsToCollectInQuests;

        private void Start()
        {
            if (m_Quests.Count >= 1)
            {
                m_CurrentQuest = m_Quests[0];
                Debug.Log(m_CurrentQuest.name);
                EventSystem.Current.RegisterListener<QuestDoneEventInfo>(QuestDone);
            }
            else
            {
                Debug.LogWarning("No quest added to Quest Manager");
            }

        }

        private void QuestDone(QuestDoneEventInfo qd)
        {
            string questDesc = qd.EventDescription;
            Debug.Log("Quest N.o." + qd.QuestID + " is completed");
            m_Quests.Remove(m_CurrentQuest);
            if (m_Quests.Count >= 1)
            {
                Debug.Log(m_CurrentQuest.name);
                m_CurrentQuest = m_Quests[0];
            }
        }


    }
}
