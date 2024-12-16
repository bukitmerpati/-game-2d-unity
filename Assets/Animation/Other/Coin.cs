using UnityEngine;

public class Coin : MonoBehaviour
{
    // Variabel untuk menyimpan nilai koin
    [SerializeField] private int value;
    
    // Variabel untuk memastikan bahwa koin hanya dipicu sekali
    private bool hasTriggered;
    
    // Referensi ke CoinManager untuk mengatur jumlah koin
    private CoinManager coinManager;
    
    // Fungsi Start dijalankan sekali ketika objek diaktifkan
    private void Start()
    {
        // Mengambil instance dari CoinManager
        coinManager = CoinManager.instance;
    }
    
    // Fungsi ini dipanggil ketika ada objek lain yang memasuki trigger collider 2D dari objek ini
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah objek yang bertabrakan memiliki tag "Player" dan koin belum dipicu
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            // Memainkan efek suara koin menggunakan AudioManager
            AudioManager.Instance.PlaySFX("koin");
            
            // Menandai bahwa koin telah dipicu
            hasTriggered = true;
            
            // Mengubah jumlah koin di CoinManager dengan nilai koin ini
            coinManager.ChangeCoins(value);
            
            // Menghancurkan objek koin ini
            Destroy(gameObject);
        }
    }
}
