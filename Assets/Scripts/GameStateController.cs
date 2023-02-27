using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private InputActionListener ActionListener;
    [SerializeField] private PlayerController PlayerController;

    public event Action OnGameOver;
    private bool isGameOver;
    private void Start()
    {
        ActionListener.OnActionKeyPressed += OnActionKeyPressed;
        PlayerController.OnDestroyed += OnDestroyed;
    }

    private void OnDestroyed(int hp)
    {
        if (hp <= 0)
        {
            isGameOver = true;
            OnGameOver?.Invoke();
        }
    }

    private void OnActionKeyPressed()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
