using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    AudioListener a;

    public GameScriptableObject gm;
    public GameObject PauseObj;
    public GameObject midGameObj;

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

    public void LoadMenu()
    {
        SceneManager.LoadScene("ChooseLevelMenu");
    }

    public void PauseGame()
    {
        gm.paused = !gm.paused;
        Physics.autoSimulation = !gm.paused;

        PauseObj.SetActive(gm.paused);
        midGameObj.SetActive(!gm.paused);
    }

}
