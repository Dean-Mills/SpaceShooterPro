using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            //Scene 1 is Game
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
