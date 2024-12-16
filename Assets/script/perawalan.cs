using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class perawalan : MonoBehaviour
{
    // Fungsi PlayGame dipanggil saat tombol Play di klik
    public void PlayGame()
    {   
        //memanggil funsi PlaySFX("button") dari AudioManager
        AudioManager.Instance.PlaySFX("button");
        // Memuat scene berikutnya berdasarkan build index dari scene saat ini ditambah 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Fungsi QuitGame dipanggil saat tombol Quit di klik
    public void QuitGame()
    {
        //memanggil funsi PlaySFX("button") dari AudioManager
        AudioManager.Instance.PlaySFX("button");
        // Menampilkan pesan "Quit!" di konsol untuk indikasi bahwa aplikasi akan keluar
        PlayerPrefs.DeleteAll();
        Debug.Log("Quit!");
        // Menutup aplikasi (berlaku untuk build stand-alone)
        Application.Quit();
    }
}
