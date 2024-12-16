using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen; // UI untuk game over
    private Transform Player; // Transform pemain
    private Vector3 startPlayerPosition; // Posisi awal pemain
    private PlayerMovement playerMovement; // Referensi ke skrip PlayerMovement

    private void Start()
    {
        // Mendapatkan referensi ke pemain berdasarkan tag
        Player = GameObject.FindWithTag("Player")?.transform;

        if (Player == null)
        {
            Debug.LogError("Player tidak ditemukan! Pastikan objek pemain memiliki tag 'Player'.");
            return;
        }

        // Menyimpan posisi awal pemain
        startPlayerPosition = Player.position;

        // Mendapatkan referensi ke skrip PlayerMovement
        playerMovement = Player.GetComponent<PlayerMovement>();

        if (gameOverScreen == null)
        {
            // Jika GameOverScreen tidak disetel, cari di hierarki
            gameOverScreen = FindObjectOfType<GameOverScreen>();
            if (gameOverScreen == null)
            {
                Debug.LogError("GameOverScreen tidak ditemukan! Pastikan objek GameOverScreen ada di scene.");
            }
        }
    }

    private void Update()
    {
        if (Player != null)
        {
            // Mengikuti posisi pemain pada sumbu X
            transform.position = new Vector3(Player.position.x, transform.position.y, -9.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cek apakah pemain sedang knockback
            if (playerMovement != null && playerMovement.canAttack())
            {
                Debug.LogWarning("Pemain sedang knockback, tidak reset posisi.");
                return;
            }

            // Mengatur ulang posisi pemain ke posisi awal
            Player.position = startPlayerPosition;

            // Reset kecepatan pemain
            Rigidbody2D playerRb = Player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }

            // Atur ulang kesehatan pemain
            HealthManager.health = 0;

            // Mainkan suara "dead" jika tersedia
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX("dead");
            }

            // Tampilkan layar game over
            if (gameOverScreen != null)
            {
                gameOverScreen.gameOver();
            }
        }
    }
}
