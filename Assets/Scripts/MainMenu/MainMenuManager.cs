﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void PlayCoop()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
