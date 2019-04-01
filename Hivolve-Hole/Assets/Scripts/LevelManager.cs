using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
        BV, BM, BD,
        VM, VD,
        MD,
        BVM, VMD, MDB, DBV,
        BVMD
    }

    struct Level
    {
        List<GameObject> objectList;
        LevelType type;
        float expectedTimeFrame; //maybe
        float dificultyLevel;
    }

    void GenerateLevel()
    {

    }

}