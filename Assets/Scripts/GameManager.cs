using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//probably want to move this out at some point...
public enum GameMode { SinglePlayer, Cooperative };

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    public GameMode gameMode;
    [SerializeField]
    private UIManager _uiManager;
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("The ui manager was not found");
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            //Scene 1 -> single player
            //Scene 2 -> coop
            SceneManager.LoadScene((gameMode == GameMode.SinglePlayer ? 1 : 2), LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "MainMenu")
                Application.Quit();
            else
                SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            _uiManager.GamePaused();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
