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

        if (GUILayout.Button("CreateRelation") && objParent != null)
        {
            AddObject(objToAdd, buffer.objectList);
            objToAdd = null;
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Finish"))
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
        string[] text = objScp.file.text.Split('\n');
        //stream.ReadToEnd();
        //stream.Close();

        //StreamWriter writer = new StreamWriter(filePath, true);

        List<string> lines = new List<string>(text); // divide by /n
        //if index bigger than size. You need to add.
        if (i >= lines.Count)
        {
            //!add lines until i reach i. Then do i.

            string line = "";
            //writer.Write("\n");

            string.Concat(line, $"{j},");
            //lines.Add($"{a},")
            //writer.Write($"{a},");

            lines.Add(line);

            text = lines.ToArray();
        }
        else //if already has objects there.
        {
            List<string> numbers = new List<string>(lines[i].Split(',')); //!this is null. Can't be null check if null

            //if already has that object related
            string line = "";
            if (numbers.IndexOf($"{j}") == -1)
            {
                string.Concat(line, $",{j}");
                //writer.Write($",{a}");
            }
            string.Concat(lines[i], line);

            text = lines.ToArray();
        }

        File.WriteAllLines(filePath, text);

        //writer.Close();
    }

    int ObjectExists(GameObject obj, List<GameObject> list)
    {
        return list.FindIndex(o => o == obj);
    }
}
