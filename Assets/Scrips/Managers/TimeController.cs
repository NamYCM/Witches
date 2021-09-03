using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class TimeController : SingletonMono<TimeController> 
    {
        public float LocalTimeScale = 1f;

        public float DeltaTime
        {
            get
            {
                return Time.deltaTime * LocalTimeScale;
            }
        }

        public bool IsPaused
        {
            get
            {
                return LocalTimeScale == 0f;
            }
        }

        public float TimeScale
        {
            get
            {
                return Time.timeScale * LocalTimeScale;
            }
        }
    }

}
