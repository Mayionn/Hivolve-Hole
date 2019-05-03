using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevels : MonoBehaviour
{
    public int levelIndex;

    public void Load()
    {
        LoadLevel(levelIndex);
    }

    void LoadLevel(int x)
    {
        if (x == -1)
            SceneManager.LoadScene("ChooseLevelMenu");
        else
            SceneManager.LoadScene($"Nivel{x}");
    }
}
