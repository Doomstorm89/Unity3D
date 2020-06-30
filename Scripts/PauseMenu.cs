using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject _PauseMenu;           // Для прикрепления меню паузы
    public static bool isPaused = false;    // для паузы игры
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // по нажатию на ESCAPE вызывается меню паузы
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    /// <summary>
    /// Метод возобновления игры из меню (нажатие на кнопку RESUME)
    /// </summary>
    public void Resume()
    {
        _PauseMenu.SetActive(false);
        Time.timeScale = 1f;            // возвращаем нормальное течение времени
        isPaused = false;
    }
    /// <summary>
    /// Игра на паузе (игра на фоне приостановлена, течение времени 0)
    /// </summary>
    private void Pause()                
    {
        _PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    /// <summary>
    /// Нажатие на кнопку "MENU" загрузит меню
    /// </summary>
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    /// <summary>
    /// Выход из игры
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
