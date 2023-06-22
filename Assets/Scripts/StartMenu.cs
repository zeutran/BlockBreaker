using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("FirstGame"))
        {
            SetDefaultData();
        }
    }

    private void SetDefaultData()
    {
        PlayerPrefs.SetInt("FirstGame", 1);
        PlayerPrefs.SetInt("GameLevel", 1);
    }

    public void MenuFlowControl(int selection) // ToDo: This method should be added to the button's OnClick event.
    {
        switch (selection)
        {
            case 0:
                SceneManager.LoadScene(1); // Start Game
                break;
            case 1:
                Application.Quit(); // Quit Game
                break;
        }
    }
}
