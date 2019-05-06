﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnTouch : MonoBehaviour
{
    public CameraSystem cam;
    public GameObject parent;
    public GameObject cylinder;

    private Vector3 targetScale;
    private Vector3 currentScale;

    public bool isEndless;

    public GameScriptableObject gm;

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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Button/Normal"))
        {
            other.gameObject.GetComponent<LoadLevels>().Load();
        }
        else
        {
            int tmp = ObjectSystem.IsObjectHittable(other.gameObject.tag);
            if (tmp != -1)
            {
                if (!isEndless)
                {

                    if (PowerupSystem.IsCurrentPowerup(PowerupSystem.Powerups.DoubleSize))
                    {
                        targetScale += new Vector3(other.attachedRigidbody.mass * 2, 0f, other.attachedRigidbody.mass * 2);
                    }
                    else
                        targetScale += new Vector3(other.attachedRigidbody.mass, 0f, other.attachedRigidbody.mass);

                    cylinder.transform.localScale += new Vector3(0, 0.5f, 0);

                    cam.newTargetVector();
                    gm.levelGameObjects.Remove(other.gameObject.transform); //!BUG HERE

                }

                Destroy(other.gameObject);
                PowerupSystem.ChoosePowerup(tmp);

            }
        }
    }
}
