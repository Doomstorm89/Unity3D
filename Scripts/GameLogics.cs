using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogics : MonoBehaviour
{
    public GameObject Asteroid;     // объект астероида
    public Text scoreText;          // текст результата/набранных очков
    public int AsteroidStartCount;  // начальное количество астероидов 
    public float SecToFirstAsteroidSpawn;   // секунды до появления первой волны астероидов
    //public float SecBetweenAsteroidSpawn; // секунды между созданием астероидов в рамках одной волны
    public float SecBetweenAsteroidWaves;   // секунды между волнами астероидов
    
    private AsteroidSpawnNew asteroidSpawnNew;  // для получение компонента спауна астероидов    
    private Player player;                      // для использования данных из скрипта игрока
    private int score;                          // результат игрока
    private int asteroidsCount;                 // кол-во созданных астероидов
    private int destroyedAsteroidsCount;        // кол-во уничтоженных астероидов
    private int waveCount;                      // счетчик волн
    
    private void Start()
    {
        GameObject asteroidSpawnObject = GameObject.FindGameObjectWithTag("AsteroidSpawnNew");  // место спауна астероидов
        asteroidSpawnNew = asteroidSpawnObject.GetComponent<AsteroidSpawnNew>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");                   
        player = playerObject.GetComponent<Player>();
        score = 0;                              // начальный счёт очков
        asteroidsCount = 0;                     // кол-во заспауненных астероидов
        destroyedAsteroidsCount = 0;            // кол-во уничтоженных астероидов
        waveCount = 0;                          // кол-во волн (в данный момент нужен только для нулевой волны)
        StartCoroutine (SpawnAsteroids());      // конвейер астероидов          
        UpdateScore();                          // обновление результата игрока              

    }

    private void Update()
    {
        PlayerDeathCheck();         // проверка жив ли игрок
    }
    /// <summary>
    /// Метод, создающий астероиды (конвейер астероидов)
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(SecToFirstAsteroidSpawn);   // ожидание до первой волны        
        while (true) 
        {                      
            for (int i = 0; i < AsteroidStartCount; i++)
            {
                if (waveCount == 0)                                 // спаун 2х крупных астероидов нулевой волны
                {
                    asteroidSpawnNew.CreateLargeAsteroid();
                    asteroidSpawnNew.CreateLargeAsteroid();                    
                    waveCount++;
                    AsteroidStartCount++;
                }
                else if (asteroidsCount > 0 && asteroidsCount == destroyedAsteroidsCount)   // если уничтожены астероиды, то спаун новых
                {
                    asteroidsCount = 0;                                                     // обнуление счетчиков для новой волны
                    destroyedAsteroidsCount = 0;
                    for (int j = 0; j < AsteroidStartCount; j++)
                    {
                        asteroidSpawnNew.CreateRandomAsteroid();
                    }
                    AsteroidStartCount++;
                    waveCount++;                    
                }
                //yield return new WaitForSeconds(SecBetweenAsteroidSpawn); // секунды между созданием астероидов в рамках одной волны                    

            }
            yield return new WaitForSeconds(SecBetweenAsteroidWaves);   // ожидание между волнами          
        }
        
    }
    /// <summary>
    /// Метод, увеличивающий счетчик созданных астероидов
    /// </summary>
    public void IncrementAsteroidsCount()
    {
        asteroidsCount += 1;
    }
    /// <summary>
    /// Метод, увеличивающий счётчик созданных астероидов на howMuch
    /// </summary>
    /// <param name="howMuch">На какое кол-во увеличить</param>
    public void IncrementAsteroidsCountBy(int howMuch)
    {
        asteroidsCount += howMuch;
    }
    /// <summary>
    /// Увеличение счетчика уничтоженных астероидов
    /// </summary>
    public void IncrementDestroyedAsteroidCount()
    {
        destroyedAsteroidsCount += 1;
    }
    
    /// <summary>
    /// Метод, обновляющий результат игрока 
    /// </summary>
    /// <param name="changedScore"></param>
    public void GetScore(int changedScore)
    {
        score += changedScore;
        UpdateScore();
    }
    /// <summary>
    /// Метод, обновляющий текст с результатом
    /// </summary>
    public void UpdateScore()
    {
        scoreText.text = "Score: " + score + "\nTry Left: " + player.GetPlayerTryCount();
    }
    /// <summary>
    /// Проверка смерти игрока, если мёртв - то перезагрузка уровня по нажатию на любую кнопку
    /// </summary>
    private void PlayerDeathCheck()
    {
        if (player.IsPlayerDead())
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}
