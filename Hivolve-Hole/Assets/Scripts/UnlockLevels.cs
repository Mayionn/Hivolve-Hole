using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> list;

    public GameScriptableObject gm;

    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            list.Add(transform.GetChild(i).gameObject);
        }

        UnlockAll();
        LockFromIndex(gm.lastLevelCompleted);
    }

    void LockFromIndex(int a)
    {
        for (int i = a + 1; i < transform.childCount; i++)
        {
            list[i].tag = "Untagged";
        }
    }

    void UnlockAll()
    {
        foreach (var item in list)
        {
            item.tag = "Button/Normal";
        }
    }
}
