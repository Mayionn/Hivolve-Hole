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
    public string filePath;
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
            //Adicionar os objectos a lista total.
            //Escrever para o ficheiro as relaçoes. AB e BA para cada objecto.

            ResetAll();
        }
    }

    void ResetAll()
    {

    }

    int AddObject(GameObject a, List<GameObject> list)
    {
        if (ObjectExists(a, list) == -1)
        {
            list.Add(a);
        }

        return ObjectExists(a, list);
    }

    void WriteRelations(int i, int[] j)
    {
        //StreamReader stream = new StreamReader(filePath); // all text.
        string[] text = File.ReadAllLines(filePath);
        //stream.ReadToEnd();
        //stream.Close();

        //StreamWriter writer = new StreamWriter(filePath, true);

        List<string> lines = new List<string>(text); // divide by /n
        //if index bigger than size. You need to add.
        if (i >= lines.Count)
        {
            string line = "";
            //writer.Write("\n");
            foreach (int a in j)
            {
                string.Concat(line, $"{a},");
                //lines.Add($"{a},")
                //writer.Write($"{a},");
            }
            lines.Add(line);

            text = lines.ToArray();
        }
        else //if already has objects there.
        {
            List<string> numbers = new List<string>(lines[i].Split(','));

            //if already has that object related
            foreach (int a in j)
            {
                string line = "";
                if (numbers.FindIndex(o => int.Parse(o) == a) == -1)
                {
                    string.Concat(line, $",{a}");
                    //writer.Write($",{a}");
                }

                string.Concat(lines[i], line);
            }
        }

        File.WriteAllLines(filePath, text);

        //writer.Close();
    }

    int ObjectExists(GameObject obj, List<GameObject> list)
    {
        return list.FindIndex(o => o == obj);
    }
}
