using UnityEngine;
using UnityEngine.UI;

public class sfxbutton : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Image targetButton;

    private bool isButtonActive;

    private AudioSource audioSource;  // Komponen AudioSource untuk mengatur volume

    private void Start()
    {
        // Memeriksa apakah komponen Image targetButton sudah di-set di inspector
        if (targetButton == null)
        {
            Debug.LogError("Target button image not set in the inspector!");
        }

        // Mengambil komponen AudioSource (jika ada)
        audioSource = FindObjectOfType<AudioSource>();

        // Memuat status tombol dari PlayerPrefs
        isButtonActive = PlayerPrefs.GetInt("SFXButtonStatus", 1) == 1;

        // Mengatur volume berdasarkan status tombol
        UpdateVolume();

        // Mengatur sprite awal berdasarkan status tombol
        UpdateButtonSprite();
    }

    public void ToggleButton()
    {
        // Memantulkan status tombol
        isButtonActive = !isButtonActive;
        
        // Memperbarui sprite tombol dan volume
        UpdateButtonSprite();
        UpdateVolume();

        // Menyimpan status tombol ke PlayerPrefs
        PlayerPrefs.SetInt("SFXButtonStatus", isButtonActive ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateButtonSprite()
    {
        // Memilih sprite yang sesuai berdasarkan status tombol
        if (isButtonActive)
        {
            targetButton.sprite = activeSprite; // Menggunakan sprite aktif
        }
        else
        {
            targetButton.sprite = inactiveSprite; // Menggunakan sprite tidak aktif
        }
    }

    private void UpdateVolume()
    {
        if (audioSource != null)
        {
            // Jika tombol aktif, set volume ke 1 (maksimal), jika tidak aktif, set volume ke 0 (mati)
            audioSource.volume = isButtonActive ? 1f : 0f;
        }
    }
}
