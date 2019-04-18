using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadEndless()
    {
        SceneManager.LoadScene("EndlessSystem");
    }
}
