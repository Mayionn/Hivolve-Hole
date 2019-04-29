using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBurn : MonoBehaviour
{

    // How do i do the fire. When it hits it despawns the object and spawns a lot of Ash Objects.
    public GameObject ashParent;
    public int ashCount;
    private int ashSpawned;

    private bool started, startAnimation;

    void OnTriggerStay(Collider col)
    {
        int tmp = ObjectSystem.IsObjectHittable(col.gameObject.tag);
        if (tmp == 4)
        {
            Destroy(col.gameObject);

            for (int i = 0; i < ashCount; i++)
            {
                Vector3 pos = col.transform.position + new Vector3(1, 1, 1) * Random.Range(0.5f, 0.9f);
                Instantiate(ashParent, pos, col.transform.rotation);
            }

        }
    }

}
