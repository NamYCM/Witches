using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class DeadzoneManager : SingleSubject<DeadzoneManager>
    {
        private void OnTriggerEnter (Collider other)
        {
            SendMessage(Deadzone.Enter);
        }
    }

    public enum Deadzone
    {
        Enter
    }

}
