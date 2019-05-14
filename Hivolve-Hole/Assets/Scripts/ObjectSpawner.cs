using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class QuestManager
{
    public GameObject player;

    public GameObject text;

    public GameObject goodCondition, badCondition;

    Vector3 RandomPosition()
    {
        var v2 = Random.insideUnitCircle;

        return player.transform.position + new Vector3(v2.x, 1, v2.y) * circleRadius;
    }

    void SpawnObjectsPerQuest()
    {

        /* Vector3 scaleAjusted;

         GameObject tmp = objScp.objectList[(int)currentQuest.objects.x];
         Vector3 b = tmp.GetComponent<Renderer>().bounds.size;
         GameObject tmp2 = objScp.objectList[(int)currentQuest.objects.y];
         Vector3 d = tmp.GetComponent<Renderer>().bounds.size;*/

        for (int i = 0; i < currentQuest.numberCondition.x; i++)
        {
            var a = Instantiate(objScp.objectList[(int)currentQuest.objects.x], RandomPosition(), Quaternion.identity, goodCondition.transform);
            a.tag = "Object/Normal";
            a.gameObject.transform.localScale = new Vector3(1, 1, 1);
            a.layer = 9;
        }
        for (int i = 0; i < currentQuest.numberCondition.y; i++)
        {
            var a = Instantiate(objScp.objectList[(int)currentQuest.objects.y], RandomPosition(), Quaternion.identity, badCondition.transform);
            a.layer = 9;
            a.tag = "Object/Normal";
            a.gameObject.transform.localScale = new Vector3(1, 1, 1); ;
        }
    }

    void UpdateText()
    {
        switch (currentQuest.type)
        {
            case 0:
                currentQuest.questText = $"Eat {currentQuest.numberCondition.x} {objScp.objectList[(int)currentQuest.objects.x].name} \nDon't Eat {currentQuest.numberCondition.y} {objScp.objectList[(int)currentQuest.objects.y].name}";
                break;
            case 1:
                currentQuest.questText = $"Burn {currentQuest.numberCondition.x} {objScp.objectList[(int)currentQuest.objects.x].name} \nCan't stop burning";
                break;
            case 2:
                currentQuest.questText = $"Eat {currentQuest.numberCondition.x} {objScp.objectList[(int)currentQuest.objects.x].name} in {currentQuest.numberCondition.y} seconds";
                break;
        }

        text.GetComponent<TextMeshProUGUI>().SetText($"Quest: \n{currentQuest.questText}");
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
                if (PowerupSystem.GetCurrentPowerup() == PowerupSystem.Powerups.None)
                {
                    currentQuest.currentEaten.y = -1;
                }
                break;
            case 2:
                currentQuest.currentEaten.x = currentQuest.numberCondition.x - goodCondition.transform.childCount;
                currentQuest.timePassed += Time.deltaTime;
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
