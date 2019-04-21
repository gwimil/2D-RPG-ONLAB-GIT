using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{
    public class EventSystem : MonoBehaviour
    {

        // Use this for initialization
        void OnEnable()
        {
            __Current = this;
        }

        static private EventSystem __Current;
        static public EventSystem Current
        {
            get
            {
                if (__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }

        delegate void EventListener(EventInfo ei);
        Dictionary<System.Type, List<EventListener>> eventListeners;

        public void RegisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }

            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }

            // Wrap a type converstion around the event listener
            EventListener wrapper = (ei) => { listener((T)ei); };

            eventListeners[eventType].Add(wrapper);
        }

        public void UnregisterListener<T>(System.Action<T> listener) where T : EventInfo
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                Debug.LogWarning("unregister before creating listeners");
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }
            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                Debug.LogWarning("unregister before register");
                eventListeners[eventType] = new List<EventListener>();
            }

            EventListener wrapper = (ei) => { listener((T)ei); };

            eventListeners[eventType].Remove(wrapper);

        }

        public void FireEvent(EventInfo eventInfo)
        {
            Debug.Log("Prepare to fire");
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                // No one is listening, we are done.
                return;
            }

            foreach (EventListener el in eventListeners[trueEventInfoClass])
            {
                Debug.Log("fired");
                el(eventInfo);
            }
        }

    }
}