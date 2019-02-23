using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    MainQuest currentmainQuest;
    MainQuest[] mainQuestsDone;
    SideQuest[] sideQuests;
    SideQuest[] sideQuestsDone;

    public QuestManager()
    {
        currentmainQuest = new MainQuest("Move your character", null);
    }
    

    public void AddsideQuests(SideQuest sq)
    {
        //TODO
    }

    public MainQuest GetMainQuest()
    {
        return currentmainQuest;
    }

    public void ChangeMainQuest(MainQuest mq)
    {
        //TODO
    }
    



}
