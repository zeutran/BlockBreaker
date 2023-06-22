using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject[] healthBars;
    public Text scoreText;
    public Text additionalScoreText;
    public GameObject additionalScoreObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreText(0);
    }

    public void UpdateAdditionalScoreText(int score)
    {
        CancelInvoke(nameof(AdditionalScoreTextReset));
        additionalScoreObject.SetActive(true);
        additionalScoreText.text = score.ToString();
        Invoke(nameof(AdditionalScoreTextReset),.7f);
    }
    private void AdditionalScoreTextReset()
    {
        additionalScoreObject.SetActive(false);
        additionalScoreText.text = "0";
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void UpdateHealthBars(int health)
    {
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (i < health)
            {
                healthBars[i].SetActive(true);
            }
            else
            {
                healthBars[i].SetActive(false);
            }
        }
    }
}