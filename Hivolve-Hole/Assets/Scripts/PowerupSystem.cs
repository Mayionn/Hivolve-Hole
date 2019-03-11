using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSystem : MonoBehaviour
{
    enum Powerups
    {
        Fire,
        Vortex,
        Magnet,
        duplica?,

    }


    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
        }
    }
}
