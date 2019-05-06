using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class QuestManager
{
    public GameObject player;

    public GameObject goodCondition, badCondition;

    Vector3 RandomPosition()
    {
        var v2 = Random.insideUnitCircle;

        return player.transform.position + new Vector3(v2.x, 1, v2.y) * circleRadius;
    }

    void SpawnObjectsPerQuest()
    {
        for (int i = 0; i < currentQuest.numberCondition.x; i++)
        {
            var a = Instantiate(objScp.objectList[(int)currentQuest.objects.x], RandomPosition(), Quaternion.identity, goodCondition.transform);
            a.tag = "Object/Normal";
            a.gameObject.transform.localScale = new Vector3(2, 2, 2);
            a.layer = 9;
        }
        for (int i = 0; i < currentQuest.numberCondition.y; i++)
        {
            var a = Instantiate(objScp.objectList[(int)currentQuest.objects.y], RandomPosition(), Quaternion.identity, badCondition.transform);
            a.layer = 9;
            a.tag = "Object/Normal";
            a.gameObject.transform.localScale = new Vector3(2, 2, 2);
        }
    }

    void UpdateQuestNumbers()
    {
        switch (currentQuest.type)
        {
            case 0:
                currentQuest.currentEaten.x = currentQuest.numberCondition.x - goodCondition.transform.childCount;
                currentQuest.currentEaten.y = currentQuest.numberCondition.y - badCondition.transform.childCount;
                break;
            case 1:
                currentQuest.currentEaten.x = currentQuest.numberCondition.x - goodCondition.transform.childCount;
                currentQuest.currentEaten.y += Time.deltaTime;
                break;
            case 2:
                currentQuest.currentEaten.x = currentQuest.numberCondition.x - goodCondition.transform.childCount;
                currentQuest.timePassed = Time.deltaTime;
                break;
        }

    }

    void DestroyChild()
    {
        for (int i = 0; i < goodCondition.transform.childCount; i++)
        {
            Destroy(goodCondition.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < badCondition.transform.childCount; i++)
        {
            Destroy(badCondition.transform.GetChild(i).gameObject);
        }
    }
}
