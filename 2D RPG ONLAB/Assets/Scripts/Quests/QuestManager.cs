using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class QuestManager : MonoBehaviour
    {
        public static Quest m_CurrentQuest;
        public List<Quest> m_Quests;


        public Text m_QuestNumber;
        public Text m_QuestName;
        public Text m_QuestDoneSoFar;


        private int questNumberInt;
        private Guid QuestEventGuid;

        void Awake()
        {
            QuestEventGuid = new Guid();
            questNumberInt = 1;
        }

        private void Start()
        {
            m_QuestNumber.text = "Küldetések: " + questNumberInt + "/" + m_Quests.Count;
            if (m_Quests.Count >= 1)
            {

                m_CurrentQuest = Instantiate(m_Quests[0], this.transform);
                m_CurrentQuest.gameObject.name = "Quest " + m_CurrentQuest.m_QuestId;
                m_CurrentQuest.SetQuestUI(m_QuestName, m_QuestDoneSoFar);
                m_QuestNumber.text = m_CurrentQuest.m_QuestName;
                Debug.Log(m_CurrentQuest.gameObject.name);
                Debug.Log(EventSystem.Current);
                Debug.Log("init2");
                EventSystem.Current.RegisterListener<QuestDoneEventInfo>(QuestDone, ref QuestEventGuid);
            }
            else
            {
                Debug.LogWarning("No quest added to Quest Manager");
            }

        }


        private void QuestDone(QuestDoneEventInfo qd)
        {
            string questDesc = qd.EventDescription;
            m_Quests.RemoveAt(0);
            if (m_Quests.Count >= 1)
            {
                questNumberInt++;
                Debug.Log(m_CurrentQuest.name);
                m_CurrentQuest = Instantiate(m_Quests[0], this.transform);
                m_CurrentQuest.SetQuestUI(m_QuestName, m_QuestDoneSoFar);
                m_QuestNumber.text = "Küldetések: " + questNumberInt + "/" + m_Quests.Count;
            }
        }


    }
}
