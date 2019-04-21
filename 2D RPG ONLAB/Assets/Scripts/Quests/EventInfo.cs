using UnityEngine;

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

    public class UnitDeathEventInfo : EventInfo
    {
        public Mobs Unit;
        public string Killer;
        public int Level;

        /*
        Info about cause of death, our killer, etc...
        Could be a struct, readonly, etc...
        */
    }


    public class QuestDoneEventInfo : EventInfo
    {
        public string UnitName;
        public int QuestID;
    }
}