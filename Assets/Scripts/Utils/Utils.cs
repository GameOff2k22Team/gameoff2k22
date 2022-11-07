using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Vector3 RecalculateVectorWithAngle(ref Vector2 inputVector, float angle)
    {
        Vector2 newVector = new Vector2();
        newVector.x = (inputVector.x * Mathf.Cos(angle) - inputVector.y * Mathf.Sin(angle));
        newVector.y = (inputVector.x * Mathf.Sin(angle) + inputVector.y * Mathf.Cos(angle));
        inputVector = newVector;
        return inputVector;
    }
}
