using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexPull : MonoBehaviour
{
    public float Force;
    public string Tag;

    void OnTriggerStay(Collider col)
    {
        if (col.tag == Tag && col.gameObject.layer == 9)
        {
            Vector3 a = this.transform.position - col.transform.position;
            col.attachedRigidbody.AddForce(a * Force, ForceMode.Impulse);
        }
    }
}
