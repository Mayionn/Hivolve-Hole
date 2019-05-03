using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LevelSystem : EditorWindow
{
    [MenuItem("Window/LevelSystem")]
    public static void ShowWindow()
    {
        GetWindow<LevelSystem>("LevelSystem");
    }

    enum LevelType
    {
        Normal,
        Burn,
        Vortex,
        Magnet,
        Double,
    } // assume that normal type can be inbetween each of those.

    static List<LevelType> levelTypes = new List<LevelType>{
        LevelType.Normal,LevelType.Burn, LevelType.Vortex, LevelType.Magnet, LevelType.Double,
    };

    struct Level
    {
        public List<GameObject> objectList;
        //- What about this being a list of types instead. That way i don't need to have the BV and company around.
        public List<LevelType> types;
        float expectedTimeFrame; //maybe
        float dificultyLevel;

        public Level(List<LevelType> t)
        {
            types = t;
            objectList = new List<GameObject>();
            expectedTimeFrame = 0;
            dificultyLevel = 0;
        }
    }


    void OnGUI()
    {

    }

}