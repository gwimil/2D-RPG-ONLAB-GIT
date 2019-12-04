using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class KillNumberOfEnemiesQuest : Quest
    {
        public List<Mobs> m_EnemyToKill;
        public int m_NumberToKill;

        private int m_CurrentNumber;
        private List<string> m_EnemyName;

        public string m_EventEnemyName;


        private Guid DeathEventGuid;

        // Start is called before the first frame update
        void Start()
        {
            m_EnemyName = new List<string>();
            m_CurrentNumber = 0;
            for (int i = 0; i <m_EnemyToKill.Count; i++)
            {
                m_EnemyName.Add(m_EnemyToKill[i].gameObject.name);
            }
            EventSystem.Current.RegisterListener<MobDeathEventInfo>(OnUnitDied, ref DeathEventGuid);
        }



        private void OnUnitDied(MobDeathEventInfo obj)
        {
            for (int i = 0; i < m_EnemyName.Count; i++)
            {
                if (m_EnemyName[i] == obj.Unit.gameObject.name)
                {
                    m_CurrentNumber++;
                    m_QuestNumberOfDoneText.text = m_CurrentNumber.ToString();
                    Debug.Log("Killed " + m_CurrentNumber + " out of " + m_NumberToKill);

                    if (m_CurrentNumber >= m_NumberToKill)
                    {
                        MobQuestDoneEventInfo me = new MobQuestDoneEventInfo();
                        me.MobName = m_EventEnemyName;
                        EventSystem.Current.FireEvent(me);

                        QuestDoneEventInfo qd = new QuestDoneEventInfo();
                        qd.EventDescription = "Quest \"" + m_QuestName + "\" has been completed!";
                        qd.QuestID = m_QuestId;
                        EventSystem.Current.FireEvent(qd);
                        m_CurrentNumber = -1;
                        EventSystem.Current.UnregisterListener<MobDeathEventInfo>(DeathEventGuid);

                        break;
                    }

                }

                
                
            }
        }

    }
}
