using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class MagicGrassController : MonoBehaviour, IObserver
    {
        [SerializeField] private Transform targetPosition;
        private Vector3 oldPosition;
        private bool onEnter = false;
        private bool onExit = false;
        private float speed = 2f;

        // Start is called before the first frame update
        void Start()
        {
            oldPosition = transform.position;
            
            EventManager.Instance.RegisterObserver(EventMagicCircle.EnterMagicCircle, this);
            EventManager.Instance.RegisterObserver(EventMagicCircle.ExitMagicCircle, this);

            if (!targetPosition)
            {
                Debug.LogError("Lost target position of grass");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!onEnter && !onExit && !targetPosition) return;

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
            if (transform.position == targetPosition.position)
            {
                onEnter = false;
                return;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
        }

        private void OnExit()
        {
            if (transform.position == oldPosition)
            {
                onExit = false;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, oldPosition, speed * Time.deltaTime);
        }

        public void OnNotify(object key, object data)
        {
            if (!((int)data == gameObject.GetInstanceID())) return;

            if ((EventMagicCircle)key == EventMagicCircle.EnterMagicCircle)
            {
                onEnter = true;
                onExit = false;
            }
            else if ((EventMagicCircle)key == EventMagicCircle.ExitMagicCircle)
            {
                onExit = true;
                onEnter = false;
            }
        }
    }

}
