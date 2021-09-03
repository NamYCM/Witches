using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class TriggerArea : Subject
    {
        [SerializeField] private GameObject control;

        public GameObject Control {
            get => control;
        }
        
        private void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            EventManager.Instance.SendMessage(EventMagicCircle.EnterMagicCircle, Control.GetInstanceID());
            //EventManager.Instance.SendMessage(EventType.EnterMagicCircle, control.GetInstanceID());
        }

        private void OnTriggerExit(Collider other)
        {
            EventManager.Instance.SendMessage(EventMagicCircle.ExitMagicCircle, Control.GetInstanceID());
        }
    }
}
