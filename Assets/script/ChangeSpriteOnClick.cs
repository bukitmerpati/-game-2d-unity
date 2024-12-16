using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteOnClick : MonoBehaviour
{
    // Aset gambar terang yang akan digunakan sebagai sprite
    public Sprite brightSprite;  
    
    // Aset gambar gelap yang akan digunakan sebagai sprite
    public Sprite darkSprite;    

    // Referensi ke komponen SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Fungsi Start dijalankan sekali ketika objek diaktifkan
    void Start()
    {
        // Mendapatkan komponen SpriteRenderer yang terpasang pada game object ini
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Mengatur sprite awal menjadi brightSprite
        spriteRenderer.sprite = brightSprite;
    }

    // Fungsi ini dipanggil ketika objek ini diklik oleh mouse

}
