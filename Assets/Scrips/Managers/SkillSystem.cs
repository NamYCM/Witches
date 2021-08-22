using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType {
    Telekinesis
}

public class SkillSystem : MonoBehaviour, IObserverSkill
{
    public void OnNotify (SkillType notificationType) {
        if (notificationType == SkillType.Telekinesis) {
            //execute skill
        }
    }
}
