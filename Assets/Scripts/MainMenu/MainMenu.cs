﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);//Póżnej indeks zmienimy na nazwę sceny
    }

    public void PlayOnline()
    {
        Debug.Log("soon");
    }

    public void Quit()
    {
        Application.Quit();
    }
}