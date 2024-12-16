using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishPoint : MonoBehaviour
{
    // Referensi ke UI "Game Win"
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private TMP_Text coinsCollectedText; // Teks untuk menampilkan jumlah koin

    private int coinsCollected; // Jumlah koin yang dikumpulkan

    // Dipanggil saat objek masuk ke collider yang terpasang
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Memeriksa apakah objek yang masuk adalah pemain
        if (collision.CompareTag("Player"))
        {
            // Mencetak pesan "Finish" ke konsol
            Debug.Log("Finish");

            // Menampilkan UI "Game Win" dan menjeda permainan
            ShowGameWinUI();
        }
    }

    // Fungsi untuk menampilkan UI "Game Win" dan menjeda permainan
   private void ShowGameWinUI()
{
    UnlockNewLevel(); // Pastikan ini dipanggil terlebih dahulu

    if (gameWinUI != null)
    {
        gameWinUI.SetActive(true);
        DisplayCoinsCollected();
        Time.timeScale = 0f;
    }
}


    // Fungsi untuk menampilkan hasil koin yang dikumpulkan
    private void DisplayCoinsCollected()
    {
        // Mengambil nilai koin dari CoinManager
        coinsCollected = CoinManager.instance.GetCoins();

        // Memastikan teks hasil koin tidak null
        if (coinsCollectedText != null)
        {
            // Menampilkan jumlah koin yang dikumpulkan
            coinsCollectedText.text = " " + coinsCollected.ToString();
        }
        else
        {
            Debug.LogError("Coins Collected Text is not assigned!");
        }
    }

    // Fungsi untuk melanjutkan ke level berikutnya
    public void ContinueToNextLevel()
{
    Time.timeScale = 1f; // Melanjutkan permainan
    UnlockNewLevel(); // Pastikan ini dipanggil sebelum memuat level berikutnya
    LoadNextLevel();
}


    // Fungsi untuk membuka level baru
private void UnlockNewLevel()
{
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);
    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

    Debug.Log("=== Unlock New Level Debugging ===");
    Debug.Log("Current Scene Index: " + currentSceneIndex);
    Debug.Log("Reached Index Before: " + reachedIndex);
    Debug.Log("Unlocked Level Before: " + unlockedLevel);

    // Pastikan ReachedIndex diperbarui jika pemain mencapai level baru
    if (currentSceneIndex > reachedIndex)
    {
        // Update ReachedIndex dengan level yang baru tercapai
        PlayerPrefs.SetInt("ReachedIndex", currentSceneIndex);
        PlayerPrefs.SetInt("UnlockedLevel", Mathf.Max(unlockedLevel, currentSceneIndex));
        PlayerPrefs.Save();  // Menyimpan perubahan

        Debug.Log("Reached Index After: " + PlayerPrefs.GetInt("ReachedIndex"));
        Debug.Log("Unlocked Level After: " + PlayerPrefs.GetInt("UnlockedLevel"));
        Debug.Log("Level " + currentSceneIndex + " is unlocked!");
    }
    else
    {
        Debug.Log("No new level unlocked. Current level is not the last reached level.");
    }

    // Debugging tambahan untuk memastikan nilai PlayerPrefs sesuai ekspektasi
    Debug.Log("Final Reached Index: " + PlayerPrefs.GetInt("ReachedIndex"));
    Debug.Log("Final Unlocked Level: " + PlayerPrefs.GetInt("UnlockedLevel"));
}





    // Fungsi untuk memuat level berikutnya
    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No next scene found! Check your build settings.");
        }
    }
}
