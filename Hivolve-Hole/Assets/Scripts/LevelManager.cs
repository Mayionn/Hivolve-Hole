using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> ObjPowerups;
    public ObjectScriptableObject objScp;

    //? How many types of puzzles. Level ends when you eat all the objects.
    //- 1. Burn
    //- 2. Vortex
    //- 3. Magnet
    //- 4. Normal Puzzle that you need to eat smaller stuff for size.
    //- 5. Double Puzzle 
    //- 6. Mix and Match.

    //- Each level needs the list of their objects. ( generated at startup +/-)
    //- Gonna be encoded into a text file. 
    //- Read the text file to load the level.
    //- 

    enum LevelType
    {
        Normal,
        Burn,
        Vortex,
        Magnet,
        Double,
        /* BV, BM, BD,
        VB, VM, VD,
        MB, MV, MD,
        DB, DV, DM*/
    } // assume that normal type can be inbetween each of those.

    static List<LevelType> levelTypes = new List<LevelType>{
        LevelType.Normal,LevelType.Burn, LevelType.Vortex, LevelType.Magnet, LevelType.Double,
        /* LevelType.BV, LevelType.BM, LevelType.BD,
        LevelType.VB, LevelType.VM, LevelType.VD,
        LevelType.MB, LevelType.MV, LevelType.MD,
        LevelType.DB, LevelType.DV, LevelType.DM,*/
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

    void FillMinRequirements(Level lvl)
    {
        foreach (var type in lvl.types)
        {
            switch (type)
            {
                case LevelType.Normal:
                    //lvl.objectList.Add(); //Add Object to eat;
                    break;
                case LevelType.Burn:
                    //lvl.objectList.Add(); //Add Burn Object.
                    //lvl.objectList.Add(); //Add Objects to Burn
                    break;
                case LevelType.Vortex:
                    //lvl.objectList.Add(); //Add Vortex Object.
                    //lvl.objectList.Add(); //Add Objects to Pull
                    break;
                case LevelType.Magnet:
                    //lvl.objectList.Add(); //Add Magnet Object.
                    //lvl.objectList.Add(); //Add Objects to Pull
                    break;
                case LevelType.Double:
                    //lvl.objectList.Add(); //Add Double Object.
                    //lvl.objectList.Add(); //Add Objects to Eat
                    break;
            }
        }
    }

    Level GenerateLevel()
    {
        //Choose a level type
        int powerupsNum = Random.Range(1, 4);
        List<LevelType> t = new List<LevelType>();
        do
        {
            LevelType a = levelTypes[Random.Range(0, levelTypes.Count)];

            if (!t.Contains(a)) // no repeats of powerups.
            {
                powerupsNum--;
                t.Add(a);
            }
        } while (powerupsNum >= 0);

        return new Level(t);
    }

}