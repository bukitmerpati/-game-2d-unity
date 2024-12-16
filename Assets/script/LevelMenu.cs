using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

private void Awake()
{

    PlayerPrefs.Save();

    ButtonsToArray();
    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
   

    for (int i = 0; i < buttons.Length; i++)
    {
        buttons[i].interactable = false;
    }

    for (int i = 0; i < unlockedLevel; i++)
    {
        buttons[i].interactable = true;
    }
}



    public void OpenLevel(int level)
    {
        string levelName = "Level " + level;
        SceneManager.LoadScene(levelName);
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }
    }
}
