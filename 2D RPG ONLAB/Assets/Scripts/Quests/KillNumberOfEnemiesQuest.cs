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

        private Guid DeathEventGuid;

        // Start is called before the first frame update
        void Start()
        {
            m_CurrentNumber = 0;
            m_EnemyName = m_EnemyToKill.gameObject.name;
            EventSystem.Current.RegisterListener<MobDeathEventInfo>(OnUnitDied, ref DeathEventGuid);
        }



        private void OnUnitDied(MobDeathEventInfo obj)
        {
            if (m_EnemyName == obj.Unit.gameObject.name)
            {
                m_CurrentNumber++;
                m_QuestNumberOfDoneText.text = m_CurrentNumber.ToString();
                Debug.Log("Killed " + m_CurrentNumber + " out of " + m_NumberToKill);
            }

            if (m_CurrentNumber >= m_NumberToKill)
            {
                QuestDoneEventInfo qd = new QuestDoneEventInfo();
                qd.EventDescription = "Quest \"" + m_QuestName + "\" has been completed!";
                qd.UnitName = m_EnemyName;
                qd.QuestID = m_QuestId;
                EventSystem.Current.FireEvent(qd);
                m_CurrentNumber = -1;
                EventSystem.Current.UnregisterListener<MobDeathEventInfo>(DeathEventGuid);
            }
        }

    }
}
