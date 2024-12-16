using UnityEngine;

// Menandai kelas Sound sebagai Serializable agar dapat ditampilkan di Inspector
[System.Serializable]
public class Sound 
{
    // Nama dari suara
    public string name;
    
    // AudioClip yang akan dimainkan
    public AudioClip clip;
}
