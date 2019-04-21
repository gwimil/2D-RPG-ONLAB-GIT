using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class KillNumberOfEnemiesQuest : Quest
    {
        public Mobs m_EnemyToKill;
        public int m_NumberToKill;
        public Items m_QuestItem;

        private int m_CurrentNumber;
        private string m_EnemyName;


        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Quest listens");
            m_CurrentNumber = 0;
            m_EnemyName = m_EnemyToKill.gameObject.name;
            EventSystem.Current.RegisterListener<UnitDeathEventInfo>(OnUnitDied);
        }



        private void OnUnitDied(UnitDeathEventInfo obj)
        {
            Debug.Log("Killed an enemy");
            if (m_EnemyName == obj.Unit.gameObject.name)
            {
                m_CurrentNumber++;
                Debug.Log("Killed " + m_CurrentNumber + " out of " + m_NumberToKill);
            }

            if (m_CurrentNumber >= m_NumberToKill)
            {
                QuestDoneEventInfo qd = new QuestDoneEventInfo();
                qd.EventDescription = "Quest \"" + m_QuestName + "\" has been completed!";
                qd.UnitName = m_EnemyName;
                qd.QuestID = m_QuestId;
                EventSystem.Current.FireEvent(qd);

                EventSystem.Current.UnregisterListener<UnitDeathEventInfo>(Finished);
            }
        }

        private void Finished(UnitDeathEventInfo obj)
        {
            //finished
        }

    }
}
