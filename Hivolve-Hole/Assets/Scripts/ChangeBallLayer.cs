using UnityEngine;
using System.Collections;

public class ChangeBallLayer : MonoBehaviour
{

    public int LayerOnEnter; // BallInHole
    public int LayerOnExit;  // BallOnTable

    void OnTriggerEnter(Collider other)
    {
        if (ObjectSystem.IsObjectHittable(other.gameObject.tag) != -1)
        {
            other.gameObject.layer = LayerOnEnter;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ObjectSystem.IsObjectHittable(other.gameObject.tag) != -1)
        {
            other.gameObject.layer = LayerOnExit;
        }
    }
}