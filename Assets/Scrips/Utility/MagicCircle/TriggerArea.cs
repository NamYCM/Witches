using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class TriggerArea : Subject
    {
        [SerializeField] public GameObject control;

        private void Start()
        {
            RegisterObserver(EventType.EnterMagicCircle, EventManager.Instance);
            RegisterObserver(EventType.ExitMagicCircle, EventManager.Instance);
        }

        private void OnTriggerEnter(Collider other)
        {
            SendMessage(EventType.EnterMagicCircle, control.GetInstanceID());
            //EventManager.Instance.SendMessage(EventType.EnterMagicCircle, control.GetInstanceID());
        }
    }
}
