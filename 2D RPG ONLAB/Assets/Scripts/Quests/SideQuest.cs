using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventCallbacks
{
    public class SideQuest : MonoBehaviour
    {
        public Items[] rewards;
        public string description;

        public string GetDestription()
        {
            return description;
        }

        public Items[] GetItems()
        {
            return rewards;
        }
    }

}
