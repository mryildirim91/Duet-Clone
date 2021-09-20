using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool _isGameOver;
    [SerializeField]private UnityEvent _gameOverEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        _isGameOver = true;
        _gameOverEvent?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public bool StopPlaying()
    {
        if (_isGameOver)
            return true;
        return false;
    }
}
