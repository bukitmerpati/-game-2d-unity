using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class levelselection : MonoBehaviour
{
    public Button[] lvlButtons;

// Start is called before the first frame update
void Start()
{
    int levelAt = PlayerPrefs.GetInt("level", 2); /* < Change this int value to whatever your level selection build index is on your
                                                         build settings */

    for (int i = 0; i < lvlButtons.Length; i++)
    {
        if (i + 2 > levelAt)
            lvlButtons[i].interactable = false;
    }
}
}
