using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame updatepub
    public static int health =3;
    public Image[] hearts;
    public Sprite fullHeart;

    public Sprite emptyHeart;

    private void Start() {
        health = 3;
        Debug.Log("Health initialized: " + health);

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
        Debug.Log("Updating health UI. Current health: " + health);

    }
}
