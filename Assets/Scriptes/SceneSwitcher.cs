using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        // Cek jika scene yang ingin dimuat adalah scene utama
        AudioManager.Instance.PlaySFX("button");
        if (sceneName == "Utama")
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        // Jika bukan scene utama, cek apakah level yang ingin dimuat sudah di-unlock
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (SceneManager.GetActiveScene().buildIndex <= unlockedLevel)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Level belum dibuka: " + sceneName);
        }
    }
}
