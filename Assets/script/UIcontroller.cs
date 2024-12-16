using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    // Referensi ke slider untuk pengaturan volume musik dan SFX
    public Slider _musicSlider;
    public Slider _sfxSlider;

    private void Start()
    {
        // Memuat volume musik dan SFX dari PlayerPrefs jika ada
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            AudioManager.Instance.musicSource.volume = _musicSlider.value;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            AudioManager.Instance.sfxSource.volume = _sfxSlider.value;
        }
    }

    // Fungsi untuk mematikan atau menyalakan musik
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    // Fungsi untuk mematikan atau menyalakan efek suara (SFX)
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    // Fungsi untuk mengatur volume musik berdasarkan nilai dari slider
    public void MusicVolume()
    {
        AudioManager.Instance.musicSource.volume = _musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);  // Menyimpan volume musik
        PlayerPrefs.Save();
    }

    // Fungsi untuk mengatur volume SFX berdasarkan nilai dari slider
    public void SFXVolume()
    {
        AudioManager.Instance.sfxSource.volume = _sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);  // Menyimpan volume SFX
        PlayerPrefs.Save();
    }
}
