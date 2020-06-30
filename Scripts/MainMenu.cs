using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    /// <summary>
    /// Метод для нажатия на кнопку PLAY в меню (запускает уровень)
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }
    /// <summary>
    /// Метод для нажатия на кнопку QUIT в меня (выход из игры)
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
