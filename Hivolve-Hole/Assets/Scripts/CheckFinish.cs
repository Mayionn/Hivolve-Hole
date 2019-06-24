using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class CheckFinish : MonoBehaviour
{
    // Start is called before the first frame update
    public GameScriptableObject gm;
    public int levelIndex;

    void Start()
    {
        gm.levelGameObjects = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            gm.levelGameObjects.Add(transform.GetChild(i));
        }

        Advertisement.Show();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            gm.finished = true;
            if (levelIndex > gm.lastLevelCompleted)
                gm.lastLevelCompleted = levelIndex;
        }
    }
}
