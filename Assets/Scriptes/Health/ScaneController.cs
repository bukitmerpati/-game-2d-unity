using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaneController : MonoBehaviour
{
       public static ScaneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   // Dalam ScaneController
public void NextLevel()
{
    int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("UnlockedLevel", Mathf.Max(unlockedLevel, nextLevelIndex));
        PlayerPrefs.Save();
        Debug.Log("Level Unlocked: " + PlayerPrefs.GetInt("UnlockedLevel"));
        SceneManager.LoadScene(nextLevelIndex);
    }
    else
    {
        Debug.LogError("No next scene found! Check your build settings.");
    }
}



    // Fungsi untuk memuat scene berdasarkan nama
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
