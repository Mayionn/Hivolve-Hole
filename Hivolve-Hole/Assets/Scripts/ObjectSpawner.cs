using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class QuestManager
{
    public GameObject player;

    Vector3 RandomPosition()
    {
        var v2 = Random.insideUnitCircle;

        return this.transform.position + new Vector3(v2.x, 1, v2.y) * circleRadius;
    }

    void SpawnObjectsPerQuest()
    {
        for (int i = 0; i < currentQuest.numberCondition.x; i++)
        {
            Instantiate(objScp.objectList[(int)currentQuest.objects.x], RandomPosition(), Quaternion.identity, this.transform);
        }
        for (int i = 0; i < currentQuest.numberCondition.y; i++)
        {
            Instantiate(objScp.objectList[(int)currentQuest.objects.y], RandomPosition(), Quaternion.identity, this.transform);
        }
    }
}
