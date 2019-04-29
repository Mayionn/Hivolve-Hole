using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScale : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale = new Vector3(
            target.transform.localScale.x,
             target.transform.localScale.x,
              target.transform.localScale.z
              );
    }
}
