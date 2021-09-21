using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score;
    [SerializeField] private Text _scoreText;
    
    public void UpdateScore()
    {
        if(GameManager.Instance.StopPlaying()) return;
        _score++;
        _scoreText.text = _score.ToString();
    }
}
