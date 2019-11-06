using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class FindPlaceQuest : Quest
    {

        public string m_PlaceName;
        private Guid PlaceFoundEventGuid;

        // Start is called before the first frame update
        void Start()
        {
            EventSystem.Current.RegisterListener<PlaceFoundEventInfo>(onPlaceFound, ref PlaceFoundEventGuid);
        }

        private void onPlaceFound(PlaceFoundEventInfo info)
        {
            if (m_PlaceName == info.PlaceName)
            {
                QuestDoneEventInfo qd = new QuestDoneEventInfo();
                qd.EventDescription = "Quest \"" + m_QuestName + "\" has been completed!";
                qd.QuestID = m_QuestId;
                EventSystem.Current.FireEvent(qd);
                EventSystem.Current.UnregisterListener<PlaceFoundEventInfo>(PlaceFoundEventGuid);
            }
        }

    }
}