using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    
    public GameObject gameOverUI;
    public GameObject player;
    
    
    // Update is called once per frame




    public void gameOver()
    {

        gameOverUI.SetActive(true);
    CoinManager.instance.UpdateGameOverUI();
        Debug.Log("Game Over");
    }

    
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void manMenu()
    {
        SceneManager.LoadScene("Utama");
    }
    

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
}
