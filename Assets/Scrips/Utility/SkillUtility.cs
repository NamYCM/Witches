using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICallback
{
 
    void Invoke();
 
}

public class SkillUtility
{
    public static Vector3 GetForceWithEqualZ (Vector3 beginPosition, Vector3 targetPosition, float targetForce) {
        Vector3 force = new Vector3();
        Vector3 direction = targetPosition - beginPosition;
        
        //Caculator new Vector have magnitude = targetForce
        float x = direction.x;
        float y = direction.y;

        float newX = Mathf.Sqrt(targetForce * targetForce / (1 + (y / x) * (y / x)));
        if (newX * x < 0) newX = -newX;
        float newY = y * newX / x;
        Vector3 temp = new Vector3 (newX, newY, direction.z);
        
        //Take the suitable vector
        if (targetForce >= direction.magnitude) force = direction;
        else force = temp;
        
        return force;
    }

    public static bool GetMousePositionAtPlane (Plane plane, out Vector3 mousePosition) {
        Ray raySelection = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        float distance;

        if (plane.Raycast(raySelection, out distance)) {
            mousePosition = raySelection.GetPoint(distance);
            return true;
        }
        
        mousePosition = Vector3.zero;
        return false;
    }
}
