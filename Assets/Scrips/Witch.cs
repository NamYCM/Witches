using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witches {
    public class Witch : ThirdPersonController<Witch> {
        [System.Serializable]
        public struct Stats {
            [Header("Movement Settings")]
            [Min(0)] public int Mana;
            
            [Min(0)] public int MaxMana;

            [Min(0)] public int Health;

            [Min(0)] public int MaxHealth;

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
            Mana = 150,
            MaxMana = 150,
            Health = 150,
            MaxHealth = 150,
            Power = 5       
        };

        private Telekinesis telekinesis = new Telekinesis();
        public float offsetMoveOnSkill = 1f; 

        public Stats Stat { 
            get {return stat; } 
            private set {stat = value; } 
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update() {
            base.Update();
                
            ExecuteSkill();
        }
        

        private void ExecuteSkill () {
            if (InputManager.Instance.onSkill) {
                // telekinesis.Execute();
                SendMessage(SkillType.Telekinesis);
                if (!CanSprint) return;
                MoveSpeed -= offsetMoveOnSkill;
                CanSprint = false;
            }
            else if (!CanSprint) {
                CanSprint = true;
                MoveSpeed += offsetMoveOnSkill;
            }
        }
    }
}
