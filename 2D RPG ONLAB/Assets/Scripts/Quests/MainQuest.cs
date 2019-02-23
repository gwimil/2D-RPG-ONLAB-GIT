using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuest : MonoBehaviour
{
    private static int questNumber = 1;
    Items[] rewards;
    string description;
    public int questID;

    public MainQuest(string desc, Items[] rew)
    {
        rewards = rew;
        description = desc;
        questID = questNumber;
        questNumber++;

    }

    public string GetDestription()
    {
        return description;
    }



}
