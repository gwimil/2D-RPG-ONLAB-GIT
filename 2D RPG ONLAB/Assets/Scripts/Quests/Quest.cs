using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EventCallbacks
{
    public class Quest : MonoBehaviour
    {
        [HideInInspector] public Text m_QuestNameText;
        [HideInInspector] public Text m_QuestNumberOfDoneText;
        public int m_QuestId;
        public string m_QuestName;

        public void SetQuestUI(Text name, Text done)
        {
            m_QuestNameText = name;
            m_QuestNumberOfDoneText = done;
            m_QuestNameText.text = m_QuestName;
            m_QuestNumberOfDoneText.text = "0";
        }
    }
}