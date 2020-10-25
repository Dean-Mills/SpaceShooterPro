using System.Collections;
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
    private Image _livesPlayer1;
    [SerializeField]
    private Image _livesPlayer2;
    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverBanner.gameObject.SetActive(false);
        _livesPlayer1.sprite = _livesSprites[3];
        _livesPlayer2.sprite = _livesSprites[3];
    }


    public void UpdatePlayerScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int player, int currentLives)
    {
        if(player == 1)
        {
            _livesPlayer1.sprite = _livesSprites[currentLives >= 0 ? currentLives : 0];
        }
        else
        {
            _livesPlayer2.sprite = _livesSprites[currentLives >= 0 ? currentLives : 0];
        }
        
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
