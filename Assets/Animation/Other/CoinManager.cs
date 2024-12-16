using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    private int coins;

    [SerializeField] private TMP_Text coinsDisplay;
    [SerializeField] private TMP_Text gameOverCoinsDisplay; // Referensi ke teks TMP untuk game over UI

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        coinsDisplay.text = coins.ToString();
    }

    public void ChangeCoins(int amount)
    {
        coins += amount;
    }

    public void UpdateGameOverUI()
    {
        if (gameOverCoinsDisplay != null)
        {
            gameOverCoinsDisplay.text = coins.ToString();
        }
    }

    public int GetCoins()
    {
        return coins;
    }
}
