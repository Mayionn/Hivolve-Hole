using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
    // Start is called before the first frame update
    public GameScriptableObject gm;

    void Start()
    {
        gm.levelGameObjects = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            gm.levelGameObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.levelGameObjects.Count == 0)
        {
            gm.finished = true;
        }
    }
}
