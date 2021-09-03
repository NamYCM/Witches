using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witches
{
    public class EffectController : MonoBehaviour, IObserver
    {
        [SerializeField] private ParticleSystem executeSkillFX;

        private bool onExecuteSkill = false;

        // Start is called before the first frame update
        void Start()
        {
            InputManager.Instance.RegisterObserver(Input.Skill, this);

            if (!executeSkillFX)
            {
                Debug.LogError("Do not have execute skill effect");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!onExecuteSkill || !executeSkillFX)
            {
                return;
            }

            // executeSkillFX.transform.position = 
        }

        public void OnNotify(object key, object data)
        {
            if ((Input)key == Input.Skill)
            {
                if (!executeSkillFX)
                {
                    return;
                }

                if ((bool)data == true)
                {
                    onExecuteSkill = true;
                    executeSkillFX.Play();
                }
                else
                {
                    onExecuteSkill = false;
                    executeSkillFX.Stop();
                }
            }
        }
    }
}
