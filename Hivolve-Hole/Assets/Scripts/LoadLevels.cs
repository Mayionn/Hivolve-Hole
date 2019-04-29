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
        SceneManager.LoadScene($"Nivel{x}");
    }
}
