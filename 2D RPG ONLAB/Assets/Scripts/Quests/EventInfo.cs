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

    public class ItemPickupEventInfo : EventInfo
    {
        public string HeroName;
    }

    public class SpawnerDeathEventInfo : EventInfo
    {
        public string Killer;
        public int Level;
    }

    public class PlaceFoundEventInfo : EventInfo
    {
        public string PlaceName;
    }

    public class MobDeathEventInfo : SpawnerDeathEventInfo
    {
        public Mobs Unit;
    }

    public class HeroDiedEventInfo : SpawnerDeathEventInfo
    {
      public string HeroName;
    }

  public class MobQuestDoneEventInfo : SpawnerDeathEventInfo
    {
        public string MobName;
    }

    public class TeleportEventInfo : EventInfo
    {
        public string teleportName;
        public Player2D playerToTeleport;
    }




    public class QuestDoneEventInfo : EventInfo
    {
        public int QuestID;
    }
}