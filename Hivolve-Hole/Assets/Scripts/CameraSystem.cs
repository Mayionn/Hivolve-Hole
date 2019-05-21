using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CameraSystem : ScriptableObject
{
    public Vector3 targetVector, startVector, maxVector;

    public void newTargetVector()
    {
        var tmp = targetVector;
        targetVector += startVector / 6;

        if (targetVector.magnitude > maxVector.magnitude)
        {
            targetVector = tmp;
        }
    }
}
