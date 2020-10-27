using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _highScoreText;
    [SerializeField]
    private Text _gameOverBanner;
    [SerializeField]
    private Image _livesPlayer1;
    [SerializeField]
    private Image _livesPlayer2;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private GameObject _pauseMenu;
    private Animator _pauseMenuAnimator;
    private int _highScore;
    // Start is called before the first frame update

    private string _highScoreKey = "HighScore";
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _highScore = PlayerPrefs.GetInt(_highScoreKey,0);
        _highScoreText.text = "High Score: " + _highScore;
        _gameOverBanner.gameObject.SetActive(false);
        _livesPlayer1.sprite = _livesSprites[3];
        _livesPlayer2.sprite = _livesSprites[3];
        _pauseMenuAnimator = _pauseMenu.GetComponent<Animator>();
        if(_pauseMenuAnimator == null)
        {
            Debug.LogError("Could not find pause menu animator");
        }
        _pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }


    public void UpdatePlayerScore(int score)
    {
        _scoreText.text = "Score: " + score;
        _highScore = (score >= _highScore) ? score : _highScore;
        _highScoreText.text = "High Score: " + _highScore;
    }

    public void GamePaused()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseMenuAnimator.SetBool("isPaused", true);
    }

    public void GameResumed()
    {
        _pauseMenuAnimator.SetBool("isPaused", false);
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt(_highScoreKey, _highScore);
    }

    public void MainMenu()
    {
        SaveHighScore();
        _pauseMenuAnimator.SetBool("isPaused", false);
        _pauseMenu.SetActive(false);
        SceneManager.LoadScene(0, LoadSceneMode.Single);   
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
