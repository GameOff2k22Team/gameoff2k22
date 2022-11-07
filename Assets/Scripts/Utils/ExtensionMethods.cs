using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods 
{
    public static float ReturnCameraAngleCalculationInDegrees(this Camera camera)
    {
        Transform cameraTr = camera.gameObject.transform;
        Vector3 cameraXProjected = Vector3.ProjectOnPlane(cameraTr.right, Vector3.up);
        return Vector3.SignedAngle(cameraXProjected, Vector3.right, Vector3.up) * Mathf.Deg2Rad;
    }
}
