﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ObjectScriptableObject : ScriptableObject
{
    public List<GameObject> objectList;
    public TextAsset file;
}
