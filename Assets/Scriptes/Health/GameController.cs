using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Variabel untuk menyimpan posisi awal pemain
    Vector2 StartPos;
    
    // Rigidbody dari pemain
    Rigidbody2D playerRb;
    
    // Objek GameOverScreen
    public GameOverScreen gameOverScreen;

    // Status apakah pemain mati atau tidak

    
    // Dipanggil saat objek diaktifkan
    private void Awake()
    {
        // Mendapatkan komponen Rigidbody2D dari objek ini
        playerRb = GetComponent<Rigidbody2D>();
    }
    
    // Dipanggil sekali saat objek diaktifkan
    private void Start()
    {
        // Menyimpan posisi awal pemain
        StartPos = transform.position;
    }
    
    // Dipanggil saat ada objek lain masuk ke collider ini
private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        // Mengurangi health
        HealthManager.health--;
        Debug.Log("Health after collision: " + HealthManager.health);

        // Knockback
        Vector2 knockbackDirection = transform.position - collision.transform.position;
        knockbackDirection.Normalize();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(knockbackDirection * 5f, ForceMode2D.Impulse); // Sesuaikan gaya knockback
            Debug.Log("Knockback applied: " + knockbackDirection);
        }
        if (HealthManager.health <= 0)
            {
                // Memulai coroutine untuk mengatasi kematian pemain
                StartCoroutine(HandleDeath());
            }
    }
}


    // Coroutine untuk mengatasi kematian pemain
    IEnumerator HandleDeath()
    {
        // Menampilkan layar game over
        gameOverScreen.gameOver();
        
        // Memainkan efek suara ketika mati
        AudioManager.Instance.PlaySFX("dead");
        
        // Menunggu sebelum respawn
        yield return StartCoroutine(Respawn(0.5f));
        
        // Menonaktifkan objek pemain
        gameObject.SetActive(false);
    }

    // Coroutine untuk respawn pemain
    IEnumerator Respawn(float duration)
    {
        // Menonaktifkan simulasi Rigidbody untuk sementara
        playerRb.simulated = false;
        
        // Mengatur skala transformasi pemain menjadi 0,0,0
        transform.localScale = new Vector3(0, 0, 0);
        
        // Menunggu sebelum respawn
        yield return new WaitForSeconds(duration);
        
        // Mengatur posisi pemain kembali ke posisi awal
        transform.position = StartPos;
        
        // Mengatur skala transformasi pemain kembali ke 1,1,1
        transform.localScale = new Vector3(1, 1, 1);
        
        // Mengaktifkan kembali simulasi Rigidbody
        playerRb.simulated = true;
    
    }
}
