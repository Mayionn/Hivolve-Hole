using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectSystem
{
    public static string ObjectTag = "Object";

    public static string[] ObjectTagList = {
        "Object", "Object/Fire",
    };
    /*
    ? 0 Does not do anything. it's the blank object
    ? 1 is Fire
    */

    public static int IsObjectHittable(string tag)
    {
        int i = 0;
        foreach (var tmp in ObjectTagList)
        {
            if (tmp == tag)
            {
                return i;
            }
            i++;
        }
        return -1;
    }
}
