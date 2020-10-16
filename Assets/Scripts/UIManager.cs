﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverBanner;
    [SerializeField]
    private Image _lives;
    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverBanner.gameObject.SetActive(false);
        _lives.sprite = _livesSprites[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _lives.sprite = _livesSprites[currentLives];
        if(currentLives == 0)
        {
            _gameOverBanner.gameObject.SetActive(true);
            StartCoroutine(GameOverFlicker());
        }
    }

    private IEnumerator GameOverFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var hold = _gameOverBanner.gameObject.activeSelf;
            _gameOverBanner.gameObject.SetActive(!hold);
        }
    }
}
