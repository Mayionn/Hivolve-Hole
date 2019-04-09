using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CameraSystem : ScriptableObject
{
    public Vector3 targetVector; public Vector3 startVector;
    public int i = 0;

    public void newTargetVector()
    {
        i++;
        if (i >= 3)
        {
            i = 0;
            targetVector += startVector / 3;
        }
    }
}
