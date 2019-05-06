using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectSystem
{
    public static string ObjectTag = "Object";

    public static string[] ObjectTagList = {
        "Object/Normal", "Object/Fire", "Object/Vortex", "Object/Metal", "Object/Water",
        "Material/Wood", "Material/Metal",
        "Button/Endless", "Button/Normal"
    };

    /*
    ? 0 is AllObjects
    ? 1 is Fire - Cause
    ? 2 is Vortex - Cause
    ? 3 is WoodMaterial
    ? 4 is MetalMaterial
    ? 5 is Metal - Cause
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
