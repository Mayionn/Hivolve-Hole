using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassToScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().mass = this.transform.localScale.x;
    }
}
