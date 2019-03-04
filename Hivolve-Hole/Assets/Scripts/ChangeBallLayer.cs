using UnityEngine;
using System.Collections;

public class ChangeBallLayer : MonoBehaviour
{

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            other.gameObject.layer = LayerOnEnter;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            other.gameObject.layer = LayerOnExit;
        }
    }
}