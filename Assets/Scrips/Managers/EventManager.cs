using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class EventManager : SingleSubject<EventManager>
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public enum EventMagicCircle
    {
        EnterMagicCircle,
        ExitMagicCircle
    }
}
