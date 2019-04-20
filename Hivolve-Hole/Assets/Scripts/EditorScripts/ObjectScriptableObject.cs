using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ObjectScriptableObject : ScriptableObject
{
    public string filePath = @"Assets\Resources\relations.txt";
    public List<GameObject> objectList;
}
