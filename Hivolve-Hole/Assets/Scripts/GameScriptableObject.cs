using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameScriptableObject : ScriptableObject
{
    public bool paused;
    public bool finished;

    public List<GameObject> levelGameObjects;
}
