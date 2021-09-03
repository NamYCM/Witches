using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witches {
    public class Witch : ThirdPersonController<Witch>, IObserver {
        [System.Serializable]
        public struct Stats {
            [Header("Movement Settings")]
            [Min(0)] public float Mana;
            
            [Min(0)] public float MaxMana;

            [Min(0)] public float Health;

            [Min(0)] public float MaxHealth;

            [Min(0)] public int Power;

            public static Stats operator +(Stats a, Stats b) {
                return new Stats {
                    Mana = a.Mana + b.Mana,
                    Health = a.Health + b.Health,
                    Power = a.Power + b.Power
                };
            }
        }   

        [SerializeField]
        private Witch.Stats stat = new Witch.Stats {
            Mana = 10,
            MaxMana = 10,
            Health = 150,
            MaxHealth = 150,
            Power = 5       
        };

        public float offsetMoveOnSkill = 1f; 

        public Stats Stat { 
            get {return stat; } 
        }

        public int Mana {
            set {
                if (value <= 0) stat.Mana = 0;
                else if (value >= stat.MaxMana) stat.Mana = stat.MaxMana;
                else stat.Mana = value;
            }
        }

        public int Health {
            set {
                if (value <= 0) stat.Health = 0;
                else if (value >= stat.MaxHealth) stat.Health = stat.MaxHealth;
                else stat.Health = value;
            }
        }

        protected override void Start()
        {
            base.Start();
            InputManager.Instance.RegisterObserver(Input.Skill, this);
        }

        protected override void Update() {
            base.Update();
                
            ExecuteSkill();
        }

        public void OnNotify(object key, object data)
        {
            switch ((Input)key)
            {
                case Input.Skill:
                    if ((bool)data == true)
                    {
                        EnterExecuteSkill();
                    }
                    else
                    {
                        ExitExecuteSkill();
                    }
                    break;
                default:
                    Debug.LogError("input invaluate!!!");
                    break;
            }
        }
        
        private void EnterExecuteSkill ()
        {
            MoveSpeed -= offsetMoveOnSkill;
            CanSprint = false;
        }

        private void ExitExecuteSkill ()
        {
            CanSprint = true;
            MoveSpeed += offsetMoveOnSkill;
        }

        private void ExecuteSkill () {
            if (InputManager.Instance.onSkill) {
                if (stat.Mana <= 0)
                {
                    ExitExecuteSkill();
                    InputManager.Instance.SkillInput(false);
                    SendMessage(PlayerBehavior.Exhausted);

                    return;
                }
                
                SendMessage(SkillType.Telekinesis);
                
                stat.Mana -= 1 * Time.deltaTime;
            }
        }
    }

    public enum PlayerBehavior
    {
        Exhausted,
        ExecuteSkill,
        CancleSkill
    }
}
