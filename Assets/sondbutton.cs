using UnityEngine;

public class sondbutton : MonoBehaviour
{
    // Fungsi yang dipanggil ketika tombol diklik
    public void OnButtonClick()
    {
        // Memanggil efek suara untuk tombol
        AudioManager.Instance.PlaySFX("button");
    }
}
