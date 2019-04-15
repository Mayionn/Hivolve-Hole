using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    AudioListener a;

    void Start()
    {
        a = this.GetComponent<AudioListener>();
    }

    public void ToggleAudio()
    {
        a.enabled = !a.enabled;
    }
}
