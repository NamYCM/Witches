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

            EventManager.Instance.RegisterObserver(EventMagicCircle.ExitMagicCircle, this);
            EventManager.Instance.RegisterObserver(EventMagicCircle.EnterMagicCircle, this);
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

            if ((EventMagicCircle)key == EventMagicCircle.EnterMagicCircle && (int)data == triggerArea.Control.GetInstanceID())
            {
                OnEnter();
            }
            else if ((EventMagicCircle)key == EventMagicCircle.ExitMagicCircle && (int)data == triggerArea.Control.GetInstanceID())
            {
                OnExit();
            }
        }
    }
}
