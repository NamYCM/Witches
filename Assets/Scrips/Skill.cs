using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witches {
    public interface ISkill {
        void Execute();
    }

    public class Telekinesis : ISkill
    {
        private Rigidbody selected;
        private Vector3 localPositionAtForce;
        private Plane plane;

        public void Execute()
        { 
            if (selected == null && Mouse.current.leftButton.isPressed) {
                CatchTarget();
            }
            else if (!Mouse.current.leftButton.isPressed) {
                ResetTarget();
            }
            else {
                ControlTarget();
            }
        }

        public void ControlTarget() {
            //Add force from position at force on target to current position mouse
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Vector3 position;

            if (SkillUtility.GetMousePositionAtPlane(plane, out position)) {
                Vector3 positionAtForce = selected.transform.TransformPoint(localPositionAtForce);
                Vector3 forceVector = SkillUtility.GetForceWithEqualZ(positionAtForce, position, Witch.Instance.Stat.Power);
            
                selected.AddForceAtPosition(forceVector, positionAtForce, ForceMode.Impulse);
            }
            else {
                Debug.Log("Error!!! Missing plane to get mouse position");
            }
        }

        public void CatchTarget() {
           

            Ray raySelection = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitSelection;

            if (Physics.Raycast(raySelection, out hitSelection, 100f, 1 << LayerMask.NameToLayer("Object"))) {
                //Get control object where mouse position
                try {
                    selected = hitSelection.transform.GetComponent<Rigidbody>();
                }
                catch (MissingComponentException) {
                    Debug.Log("Missing rigibody in target!!!");
                    hitSelection.transform.gameObject.AddComponent<Rigidbody>();
                    selected = hitSelection.transform.GetComponent<Rigidbody>();
                    selected.drag = 5f;
                }

                //Create plane to catch mouse position at target's plane
                plane = new Plane(Vector3.forward, selected.transform.position);
                Vector3 mousePosition;

                if (SkillUtility.GetMousePositionAtPlane(plane, out mousePosition)) {
                    localPositionAtForce = selected.transform.InverseTransformPoint(mousePosition);
                }
                else {
                    Debug.Log("Error!!! Missing plane to get mouse position");
                }
            }
        }

        public void ResetTarget() {
            selected = null;
        }
    }
        
}
