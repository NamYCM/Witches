using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class WaveFXController : MonoBehaviour, IObserver
    {
        [SerializeField] private ParticleSystem waveFX;
        [SerializeField] private TriggerArea triggerArea;

        // Start is called before the first frame update
        void Start()
        {
            if (!TryGetComponent<ParticleSystem>(out waveFX))
            {
                Debug.LogError("Lost wave effect");
            }

            triggerArea = GetComponentInParent<TriggerArea>();
            if (triggerArea == null)
            {
                Debug.LogError("Lost trigger area");
            }

            EventManager.Instance.RegisterObserver(EventType.ExitMagicCircle, this);
            EventManager.Instance.RegisterObserver(EventType.EnterMagicCircle, this);
        }

        private void OnEnter()
        {
            waveFX.Play();
        }

        private void OnExit()
        {
            waveFX.Stop();
        }

        public void OnNotify(object key, object data)
        {
            if (!triggerArea && !waveFX) return;

            if ((EventType)key == EventType.EnterMagicCircle && (int)data == triggerArea.control.GetInstanceID())
            {
                OnEnter();
            }
            else if ((EventType)key == EventType.ExitMagicCircle && (int)data == triggerArea.gameObject.GetInstanceID())
            {
                OnExit();
            }
        }
    }
}
