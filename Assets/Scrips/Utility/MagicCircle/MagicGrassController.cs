using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class MagicGrassController : MonoBehaviour, IObserver
    {
        [SerializeField] private Vector3 targetPosition;
        private Vector3 oldPosition;
        private bool onEnter = false;
        private bool onExit = false;

        // Start is called before the first frame update
        void Start()
        {
            oldPosition = transform.localPosition;

            EventManager.Instance.RegisterObserver(EventType.EnterMagicCircle, this);
            EventManager.Instance.RegisterObserver(EventType.ExitMagicCircle, this);
        }

        // Update is called once per frame
        void Update()
        {
            if (!onEnter && !onExit) return;

            if (onEnter)
            {
                OnEnter();   
            }
            if (onExit)
            {
                OnExit();
            }
        }

        public void OnEnter()
        {
            if (transform.position == targetPosition)
            {
                onEnter = false;
                return;
            }

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime);
        }

        private void OnExit()
        {
            if (transform.position == oldPosition)
            {
                onExit = false;
                return;
            }

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, oldPosition, Time.deltaTime);
        }

        public void OnNotify(object key, object data)
        {
            if (!((int)data == gameObject.GetInstanceID())) return;

            if ((EventType)key == EventType.EnterMagicCircle)
            {
                onEnter = true;
                onExit = false;
            }
            else if ((EventType)key == EventType.ExitMagicCircle)
            {
                onExit = true;
                onEnter = false;
            }
        }
    }

}
