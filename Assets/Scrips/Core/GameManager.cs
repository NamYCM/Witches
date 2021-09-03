using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class GameManager : SingleSubject<GameManager>, IObserver
    {
        // Start is called before the first frame update
        void Start()
        {
            DeadzoneManager.Instance.RegisterObserver(Deadzone.Enter, this);
        }

        public void OnNotify(object key, object data)
        {
            switch ((Deadzone)key)
            {
                case Deadzone.Enter:
                    EnterDeadzone();
                    break;
                default:
                    break;
            }
        }

        private void EnterDeadzone ()
        {
            TimeController.Instance.LocalTimeScale = 0f;
            Time.timeScale = 0f;
            SendMessage(GameState.GameOver);
        }
    }

    public enum GameState
    {
        GameOver,
    }
}
