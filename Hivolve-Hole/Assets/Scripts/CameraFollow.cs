using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject targetObj;

    public Vector3 targetVector;

    void Start()
    {
        targetVector = this.transform.position - targetObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = targetVector + targetObj.transform.position;
    }
}
