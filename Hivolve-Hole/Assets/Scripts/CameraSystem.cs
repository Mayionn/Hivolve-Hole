using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CameraSystem : ScriptableObject
{
    public Vector3 targetVector, startVector;

    public void newTargetVector()
    {
        targetVector += startVector / 6;
    }
}
