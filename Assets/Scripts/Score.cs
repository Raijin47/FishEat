using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    public TMP_Text text;
    public static int bestScore;

    void Start()
    {
        Instance = this;
        bestScore = PlayerPrefs.GetInt(nameof(bestScore), 0 );
        SetText();
    }

    private void SetText()
    {
        text.text = bestScore.ToString();
    }

    public static void Save(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(nameof(bestScore), bestScore);
            Instance.SetText();
        }
    }
}
