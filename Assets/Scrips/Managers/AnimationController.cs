using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class AnimationController : MonoBehaviour, IObserver
    {
        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.RegisterObserver(GameState.GameOver, this);
        }

        public void OnNotify(object key, object data)
        {
            switch ((GameState)key)     
            {
                case GameState.GameOver:
                    //animation dead
                    break;
                default:
                    Debug.LogError("state invaluate");
                    break;
            }
        }
    }
}
