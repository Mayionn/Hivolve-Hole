using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    AudioListener audioSource;

    public GameScriptableObject gm;
    public GameObject PauseObj;
    public GameObject midGameObj;
    public GameObject endGameObj;

    void Start()
    {
        audioSource = this.GetComponent<AudioListener>();
        gm.finished = false;
    }

    void Update()
    {
        if (gm.finished)
        {
            endGameObj.SetActive(true);
            PauseObj.SetActive(false);
            midGameObj.SetActive(false);
        }
    }

    public void ToggleAudio()
    {
        audioSource.enabled = !audioSource.enabled;
        gm.muted = !gm.muted;
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
