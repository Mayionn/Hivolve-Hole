using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class Data
{
    [SerializeField]
    public int levelData;

    public Data(int l)
    {
        levelData = l;
    }
}

public class WriteSaveData : MonoBehaviour
{
    public Data dataRef;
    const string folderName = "Data.dat";

    public GameScriptableObject gm;

    public void Start()
    {
        RetrieveData();
    }

    public void RetrieveData()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
            CreateFile();
        }
        else if (!File.Exists(Application.persistentDataPath + "/SaveData/" + folderName))
            CreateFile();
        dataRef = LoadData();
    }

    private void CreateFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.persistentDataPath + "/SaveData/" + folderName, FileMode.Create);
        Data data = new Data(gm.lastLevelCompleted);
        bf.Serialize(file, data);
        file.Close();
    }

    private Data LoadData()
    {
        string path = Application.persistentDataPath + "/SaveData/" + folderName;
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            Data data = bf.Deserialize(file) as Data;
            file.Close();
            Debug.Log("File Loaded");
            return data;
        }
        else
        {
            Debug.LogError("Save File not found in: " + path);
            return null;
        }
    }
}