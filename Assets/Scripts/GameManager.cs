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
            Application.Quit();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
