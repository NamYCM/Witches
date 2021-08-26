using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witches {
    public interface ISkill {
        void Execute();
    }

    public class Telekinesis : ISkill, IObserver
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

        private void ControlTarget() {
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

        private void CatchTarget() {
           

            Ray raySelection = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitSelection;

            if (Physics.Raycast(raySelection, out hitSelection)) {
                //Get control object where mouse position
                try {
                    if (hitSelection.transform.tag == "Object") {
                        selected = hitSelection.transform.GetComponent<Rigidbody>();
                    }
                    else return;
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

        private void ResetTarget() {
            selected = null;
        }

        public void OnNotify (object key, object data) {
            if ((SkillType)key == SkillType.Telekinesis) {
                Execute();
            }
        }
    }
}
