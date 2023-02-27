using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    [SerializeField] private int ScoreIncrement = 300;

    public event Action<int> OnScoreChanged;
    
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
            }

            return _instance;
        }
    }
    
    protected void Awake()
    {
        _instance = this;
    }

    public void AddScore(int asteroidSize)
    {
        Score += (int)((float)1 / asteroidSize * ScoreIncrement);
        OnScoreChanged?.Invoke(Score);
    }
}
