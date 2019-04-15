using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CameraSystem cam;
    public GameObject targetObj;

    void Start()
    {
        cam.targetVector = cam.startVector;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(
            this.transform.position,
            cam.targetVector + targetObj.transform.position,
            Time.deltaTime * 2
        );
    }
}
