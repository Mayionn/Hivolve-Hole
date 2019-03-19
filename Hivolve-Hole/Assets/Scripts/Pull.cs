using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour
{
    public float Force;

    void OnTriggerStay(Collider col)
    {
        Vector3 a = this.transform.position - col.transform.position;
        col.attachedRigidbody.AddForce(a * Force, ForceMode.Impulse);
    }
}