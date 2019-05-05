﻿using UnityEngine;

namespace EventCallbacks
{
    public abstract class EventInfo
    {
        /*
            * The base EventInfo,
            * might have some generic text
            * for doing Debug.Log?
            */

        public string EventDescription;
    }

    public class DebugEventInfo : EventInfo
    {
        public int VerbosityLevel;
    }

    public class ItemPickupEventInfo : EventInfo
    {
        public string HeroName;
    }

    public class SpawnerDeathEventInfo : EventInfo
    {
        public string Killer;
        public int Level;
    }

    public class MobDeathEventInfo : SpawnerDeathEventInfo
    {
        public Mobs Unit;
    }




    public class QuestDoneEventInfo : EventInfo
    {
        public string UnitName;
        public int QuestID;
    }
}