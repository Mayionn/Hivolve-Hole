using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<GameObject> objectList;
    public List<GameObject> powerupEnablers;

    private string filePath = "Assets/TextFiles/relations.txt";

    [System.Serializable]
    public struct QuestObject
    {
        public GameObject himself;
        public List<int> objectRelationList;
        public string Tag;

        public QuestObject(GameObject him, string tg, List<int> relations = null)
        {
            himself = him;
            objectRelationList = relations;
            Tag = tg;
        }
    }

    public struct Quest
    {
        public Vector2 objects;
        public int type;
        public string questText;
        public float timeTotal, timePassed;
        public int completed; // 0 for StillGoing, 1 for Completed, -1 for Failed;
        public Vector2 currentEaten;
        public Vector2 numberCondition;

        public Quest(Vector2 obj, int t, string txt, Vector2 conditions)
        {
            objects = obj;
            type = t;
            questText = txt;
            timeTotal = 0;
            timePassed = 0;
            completed = 0;
            currentEaten = new Vector2();
            numberCondition = conditions;
        }

        public void UpdateText(string txt)
        {
            questText = txt;
        }

        public int IsCompleted(int helper)
        {
            completed = 0;
            switch (type)
            {
                case 0: //Eat X, can't eat Y
                    if (currentEaten.y > numberCondition.y)
                    {
                        completed = -1;
                    }
                    else if (currentEaten.x > numberCondition.x)
                    {
                        completed = 1;
                    }
                    break;
                case 1: //Burn Down X Objects. If you stop burning you lose.
                    if (numberCondition.y == -1)
                    {
                        completed = -1;
                    }
                    else if (currentEaten.x > numberCondition.x)
                    {
                        completed = 1;
                    }
                    break;
                case 2: //Don't Eat X for 1 min
                    if (timePassed - timeTotal < 0 && currentEaten.x < numberCondition.x)
                    {
                        completed = 1;
                    }
                    else
                    {
                        completed = -1;
                    }
                    break;
            }
            return completed;
        }
    }

    public List<QuestObject> questObjects;

    void FillObjects()
    {
        int i = 0;
        foreach (GameObject tmp in objectList)
        {
            questObjects.Add(
                new QuestObject(objectList[i], tmp.tag, FillRelations(i))
            );
            i++;
        }
    }

    List<int> FillRelations(int i)
    {
        List<int> relations = new List<int>();
        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);
            string[] lines = text.Split('\n');
            for (int j = 0; j < lines.Length; j++)
            {
                string[] numbers = lines[i].Split(',');
                for (int x = 0; x < numbers.Length; x++)
                {
                    if (numbers[x] == "\n")
                        break;
                    relations.Add(int.Parse(numbers[x]));
                }
            }
        }
        return relations;
    }

    Vector2 Random2RelatedObjects()
    {
        int firstObject = Random.Range(0, questObjects.Count);
        int relatedObject = Random.Range(0, questObjects[firstObject].objectRelationList.Count);

        return new Vector2(firstObject, questObjects[firstObject].objectRelationList[relatedObject]);
    }

    int ChooseQuestType()
    {
        return Random.Range(0, 2);
    }

    Vector2 CalculateConditionNumber(int questType)
    {
        return new Vector2();
    }

    void GenerateQuest()
    {
        Quest quest = new Quest();
        int type = ChooseQuestType();
        string questText;
        Vector2 conditions;
        Vector2 objectsIndex;
        switch (type)
        {
            case 0:
                objectsIndex = Random2RelatedObjects();
                conditions = CalculateConditionNumber(type);
                questText = "Eat " + conditions.x + " " + objectList[(int)objectsIndex.x].name + "\n Don't Eat " + conditions.y + " " + objectList[(int)objectsIndex.y].name;
                quest = new Quest(objectsIndex, type, questText, conditions);
                break;
            case 1:
                objectsIndex = Random2RelatedObjects();
                conditions = CalculateConditionNumber(type);
                questText = "Burn " + conditions.x + " " + objectList[(int)objectsIndex.x].name + "\n Can't stop burning";
                quest = new Quest(objectsIndex, type, questText, conditions);
                break;
            case 2:
                objectsIndex = Random2RelatedObjects();
                conditions = CalculateConditionNumber(type);
                questText = "Don't eat " + conditions.x + " " + objectList[(int)objectsIndex.x].name + " for " + conditions.y + " minutes";
                quest = new Quest(objectsIndex, type, questText, conditions);
                break;
        }

        Debug.Log(quest.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        FillObjects();
        GenerateQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
