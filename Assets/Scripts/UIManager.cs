using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score;
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateScore),10,0.5f);
    }

    private void UpdateScore()
    {
        if(GameManager.Instance.StopPlaying()) return;
        _score++;
        _scoreText.text = _score.ToString();
    }
}
