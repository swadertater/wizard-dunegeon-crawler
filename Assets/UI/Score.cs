using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _scoreText;
    private void Start()
    {
        _scoreText = GetComponent<Text>();
    }
    
    private void Update()
    {
        _scoreText.text = "Score: " + Wizard.score;
    }
}