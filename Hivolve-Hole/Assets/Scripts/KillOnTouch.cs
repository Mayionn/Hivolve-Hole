using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    public GameObject parent;

    private Vector3 targetScale;
    private Vector3 currentScale;

    public void Start()
    {
        targetScale = parent.transform.localScale;
        currentScale = targetScale;
    }

    public void Update()
    {
        currentScale = Vector3.Lerp(
                        currentScale,
                        targetScale,
                        0.1f);

        parent.transform.localScale = currentScale;
    }

    void OnTriggerEnter(Collider other)
    {
        int tmp = ObjectSystem.IsObjectHittable(other.gameObject.tag);
        if (tmp != -1)
        {
            Destroy(other.gameObject);

            PowerupSystem.ChoosePowerup(tmp);

            targetScale += new Vector3(0.5f, 0, 0.5f);
        }
    }
}
