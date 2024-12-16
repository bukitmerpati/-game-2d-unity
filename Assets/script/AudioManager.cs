using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Instance statis dari AudioManager untuk akses global
    public static AudioManager Instance;
    
    // Array untuk menyimpan suara musik dan efek suara (SFX)
    public Sound[] musicSounds, sfxSounds;
    
    // AudioSource untuk musik, efek suara, dan suara petir
    public AudioSource musicSource, sfxSource, thunderSource;

    // Menambahkan variabel untuk memantau suara 'run'
    private bool isRunSFXPlaying = false;
    
    public bool IsSFXPlaying(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s != null)
        {
            // Mengecek apakah clip sedang diputar di audio source
            return sfxSource.isPlaying && sfxSource.clip == s.clip;
        }
        return false;
    }
    private void Awake()
    {
        // Mengecek apakah instance belum diisi
        if (Instance == null)
        {
            Instance = this;  // Mengatur instance ke objek ini
            DontDestroyOnLoad(gameObject);  // Mencegah penghancuran objek ini ketika memuat scene baru
        }
        else
        {
            Destroy(gameObject);  // Menghancurkan objek duplikat
            return;
        }
    }

    // Fungsi untuk memainkan efek suara (SFX) berdasarkan nama
    public void PlaySFX(string name)
    {
        // Mencari sound yang sesuai dengan nama di array sfxSounds
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");  // Menampilkan peringatan jika sound tidak ditemukan
            return;
        }

        

        // Cek jika SFX yang dimaksud adalah "run"
        if (name == "run" && !isRunSFXPlaying)
        {
            sfxSource.PlayOneShot(s.clip);  // Memainkan efek suara sekali
            isRunSFXPlaying = true;  // Menandakan bahwa suara "run" sedang diputar
        }
        else if (name != "run")
        {
            sfxSource.PlayOneShot(s.clip);  // Memainkan efek suara lainnya
        }
    }

    // Fungsi untuk menghentikan efek suara "run"
    public void StopSFX(string name)
    {
        if (name == "run")
        {
            isRunSFXPlaying = false;  // Menandakan bahwa suara "run" sudah dihentikan
        }
    }

    // Fungsi untuk menyalakan/mematikan musik
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;  // Membalik status mute musik
    }

    // Fungsi untuk menyalakan/mematikan efek suara (SFX)
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;  // Membalik status mute SFX
    }

    // Fungsi untuk mengatur volume musik
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;  // Mengatur volume musik
    }

    // Fungsi untuk mengatur volume efek suara (SFX)
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;  // Mengatur volume SFX
    }
}
