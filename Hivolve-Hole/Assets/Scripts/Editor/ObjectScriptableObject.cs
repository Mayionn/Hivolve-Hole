using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ObjectScriptableObject : ScriptableObject
{
    public List<GameObject> objectList;
}

[CreateAssetMenu()]
public class BufferScriptableObject : ScriptableObject
{
    public List<GameObject> objectList;
}
