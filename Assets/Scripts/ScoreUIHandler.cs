using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUIHandler : MonoBehaviour
{
    private TMP_Text _scoreText;
    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        ScoreManager.Instance.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreText.text = score.ToString();
    }
    
}
