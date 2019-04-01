using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Variables")]
    public float MinDif;
    public float MaxDif;
    public int QuestsCompleted, QuestsFailed;
    [Space(10)]
    public TextAsset file;
    public List<GameObject> objectList;
    public List<GameObject> powerupEnablers;
    [Space(10)]
    public List<QuestObject> questObjects;
    Quest currentQuest;


    [System.Serializable]
    public struct QuestObject
    {
        public float himself;
        public List<int> objectRelationList;
        public string Tag;

        public QuestObject(float him, string tg, List<int> relations = null)
        {
            himself = him;
            objectRelationList = relations;
            Tag = tg;
        }
    }

    public struct Quest
    {
        public Vector2 objects, currentEaten, numberCondition;
        public int type;
        public string questText;
        public float timeTotal, timePassed;
        public int completed; // 0 for StillGoing, 1 for Completed, -1 for Failed;

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

        public int IsCompleted()
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

    void FillObjects()
    {
        int i = 0;
        foreach (GameObject tmp in objectList)
        {
            questObjects.Add(
                new QuestObject(i, tmp.tag, FillRelations(i))
            );
            i++;
        }
    }

    List<int> FillRelations(int i)
    {
        List<int> relations = new List<int>();
        if (file)
        {
            string text = file.text;
            string[] lines = text.Split('\n');

            string[] numbers = lines[i].Split(',');
            for (int x = 0; x < numbers.Length; x++)
            {
                if (numbers[x] == "\n" || numbers[x] == "\r")
                    continue;
                int l = int.Parse(numbers[x]) - 1;
                relations.Add(l);
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
        float dif = CalculateDificultyLevel();
        Vector2 retvalues = new Vector2();
        int value = (int)Random.Range(5, 10);
        switch (questType)
        {
            case 0:
                retvalues.x = value;
                retvalues.y = (int)dif * value;
                break;
            case 1:
                retvalues.x = (int)dif * value;
                break;
            case 2:
                //max 1 min
                retvalues.x = value;
                retvalues.y = (int)dif * 1 / 3;
                break;
        }
        return retvalues;
    }

    float CalculateDificultyLevel()
    {
        //So this needs to be based on time + the number of quests already done.
        //? What kind of curve do i want?
        //- Myabe a logarithm with a top value. 
        float func = Mathf.Log10(QuestsCompleted / (QuestsFailed + 1));
        float dif = 2 * func * func + 1;
        Debug.Log(dif);
        if (dif > MaxDif)
            return MaxDif;
        else if (dif < MinDif)
            return MinDif;
        else
            return dif;
    }

    Quest GenerateQuest()
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
                questText = "Eat " + conditions.x + " " + objectList[(int)objectsIndex.x].name + "\nDon't Eat " + conditions.y + " " + objectList[(int)objectsIndex.y].name;
                quest = new Quest(objectsIndex, type, questText, conditions);
                break;
            case 1:
                objectsIndex = Random2RelatedObjects();
                conditions = CalculateConditionNumber(type);
                questText = "Burn " + conditions.x + " " + objectList[(int)objectsIndex.x].name + "\nCan't stop burning";
                quest = new Quest(objectsIndex, type, questText, conditions);
                break;
            case 2:
                objectsIndex = Random2RelatedObjects();
                conditions = CalculateConditionNumber(type);
                questText = "Don't eat " + conditions.x + " " + objectList[(int)objectsIndex.x].name + " for " + conditions.y + " minutes";
                quest = new Quest(objectsIndex, type, questText, conditions);
                break;
        }

        return quest;
    }

    // Start is called before the first frame update
    void Start()
    {
        FillObjects();
        currentQuest = GenerateQuest();
        Debug.Log(currentQuest.questText);
    }

    void NewQuest()
    {
        currentQuest = GenerateQuest();
        //Debug.Log(currentQuest.questText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentQuest.completed = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentQuest.completed = -1;
        }
        switch (currentQuest.completed)
        {
            case 0:
                break;
            case 1:
                QuestsCompleted++; NewQuest();
                break;
            case -1:
                QuestsFailed++; NewQuest();
                break;
        }
    }
}
