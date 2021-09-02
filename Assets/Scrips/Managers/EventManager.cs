using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class EventManager : SingleSubject<EventManager>, IObserver
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnNotify(object key, object data)
        {
            SendMessage(key, data);
        }
    }

    public enum EventType
    {
        EnterMagicCircle,
        ExitMagicCircle
    }
}
