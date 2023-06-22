using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;
    private GameObject _currentLevel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (Resources.Load<GameObject>("Levels/" + PlayerPrefs.GetInt("GameLevel")) == null)
        {
            PlayerPrefs.SetInt("GameLevel", 1);
           
            _currentLevel =
                Instantiate(
                    Resources.Load<GameObject>("Levels/" + PlayerPrefs.GetInt("GameLevel")));
        }
        else
        {
            _currentLevel =
                Instantiate(
                    Resources.Load<GameObject>("Levels/" + PlayerPrefs.GetInt("GameLevel")));
        }
    }
    public void NewLevel()
    {
        LevelManager.instance.currentPoint = 0;
        UiManager.instance.UpdateScoreText(0);
        UiManager.instance.UpdateHealthBars(3);
        Destroy(_currentLevel);
        Resources.UnloadUnusedAssets();
        LoadLevel();
    }
}
