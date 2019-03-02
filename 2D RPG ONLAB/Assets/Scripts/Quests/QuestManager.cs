using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class QuestManager : MonoBehaviour
{

    int currentMainquestNumber = 0;
    MainQuest currentMainQuest;
    public MainQuest[] MainQuests;
    public MainQuest[] mainQuestsDone;
    

    SideQuest[] sideQuests;
    SideQuest[] sideQuestsDone;
    
    public void AddMainQuest(MainQuest mq)
    {
        //TODO
    }

    public void AddsideQuests(SideQuest sq)
    {
        //TODO
    }

    public MainQuest MainQuestDone()
    {
        //TODO
        //next mainquest
        return currentMainQuest;
    }

    public SideQuest SideQuestDone()
    {
        //TODO
        //next mainquest
        return sideQuests[0];
    }

    
}

