using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches {
    public class SkillSystem : MonoBehaviour {
        private Telekinesis telekinesis = new Telekinesis();

        void Start() {
            Witch.Instance.RegisterObserver((object)SkillType.Telekinesis, telekinesis);
        }
    }

    public enum SkillType {
        Telekinesis
    }

    
}
    
