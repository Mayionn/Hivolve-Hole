using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class RelationsEditor : EditorWindow
{
    [MenuItem("Window/RelationsCreation")]
    public static void ShowWindow()
    {
        GetWindow<RelationsEditor>("RelationsCreation");
    }

    public ObjectScriptableObject objScp; //has a list of all objects
    public BufferScriptableObject buffer; //has a buffer list of objects
    public TextAsset file;
    public string filePath = @"Assets\Resources\relations.txt";
    GameObject objParent;
    GameObject objToAdd;



    void OnGUI()
    {
        objParent = (GameObject)EditorGUILayout.ObjectField("Object", objParent, typeof(GameObject), allowSceneObjects: true);

        GUILayout.Space(10);

        SerializedObject serialize = new SerializedObject(buffer);
        serialize.Update();
        SerializedProperty property = serialize.FindProperty("objectList");

        EditorGUILayout.PropertyField(property, true);

        GUILayout.Space(10);

        objToAdd = (GameObject)EditorGUILayout.ObjectField("ObjectToRelate", objToAdd, typeof(GameObject), allowSceneObjects: true);

        GUILayout.Space(10);

        if (GUILayout.Button("CreateRelation") && objParent != null && objToAdd != null)
        {
            AddObject(objToAdd, buffer.objectList);
            objToAdd = null;
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Finish") && objParent != null && buffer.objectList.Count >= 0)
        {
            AddObject(objParent, objScp.objectList);

            int i = ObjectExists(objParent, objScp.objectList);
            //Adicionar os objectos a lista total.
            foreach (var obj in buffer.objectList)
            {
                AddObject(obj, objScp.objectList);
                int j = ObjectExists(obj, objScp.objectList);

                WriteRelations(i, j);
                WriteRelations(j, i);
            }
            //Escrever para o ficheiro as relaçoes. AB e BA para cada objecto.
            ResetAll();
        }
    }

    void ResetAll()
    {
        objParent = objToAdd = null;
        buffer.objectList.Clear();
    }

    void AddObject(GameObject a, List<GameObject> list)
    {
        if (ObjectExists(a, list) == -1)
        {
            list.Add(a);
        }
    }

    void WriteRelations(int i, int j)
    {
        //StreamReader stream = new StreamReader(filePath); // all text.
        //stream.ReadToEnd();
        //stream.Close();

        string[] text = File.ReadAllLines(filePath);

        //StreamWriter writer = new StreamWriter(filePath, true);
        //string[] text = objScp.file.text.Split('\n');

        List<string> lines = new List<string>(text); // divide by /n
                                                     //if index bigger than size. You need to add.
        if (i > lines.Count - 1)
        {
            string line = "";
            //writer.Write("\n");

            while (lines.Count <= i)
            {
                lines.Add(line);
            }

            line = string.Concat(line, $"{j}");
            //lines.Add($"{a},")
            //writer.Write($"{a},");

            lines[i] = line;

            text = lines.ToArray();
        }
        else //if already has objects there.
        {
            List<string> numbers = new List<string>(lines[i].Split(','));
            string line = "";

            //if already has that object related
            if (numbers[0].Equals(""))
            {
                line = string.Concat(line, $"{j}");
            }
            else if (numbers.IndexOf($"{j}") == -1)
            {
                line = string.Concat(line, $",{j}");
            }
            lines[i] = string.Concat(lines[i], line);

            text = lines.ToArray();
        }

        File.WriteAllLines(filePath, text);
    }

    int ObjectExists(GameObject obj, List<GameObject> list)
    {
        return list.FindIndex(o => o == obj);
    }
}