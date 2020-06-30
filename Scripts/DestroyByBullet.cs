using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBullet : MonoBehaviour
{
    public AudioClip[] AudioClips;      // массив звуков (звуки уничтожения объектов)
    public int LargeAsteroidPoints;     // очки за уничтожение крупного астероида
    public int MediumAsteroidPoints;    // очки за уничтожение среднего астероида
    public int SmallAsteroidPoints;     // очки за уничтожение малого астероида    
    
    private GameLogics gameLogics;      // для передачи данных о наборе очков
    private AsteroidSpawnNew asteroidSpawnNew;  // для создания астероидов
    private AudioSource audioSource;    // для воспроизведения звуков
    
    private void Start()
    {
        GameObject gameLogicsObject = GameObject.FindGameObjectWithTag("GameLogics");   // находим объект, отвечающий за логику
        gameLogics = gameLogicsObject.GetComponent<GameLogics>();

        GameObject asteroidSpawnObject = GameObject.FindGameObjectWithTag("AsteroidSpawnNew");  // находим объект, отвечающий за 
        asteroidSpawnNew = asteroidSpawnObject.GetComponent<AsteroidSpawnNew>();                // спаун астероидов

        audioSource = GetComponent<AudioSource>();                                      // подключаем воспроизведение звуков
    }
    
    /// <summary>
    /// Проверка столкновения пули с астероидом, игрока с астероидом и НЛО. Начисление очков
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Bullet") || other.CompareTag("Player"))
        {
            if (other.CompareTag("Bullet"))
            Destroy(other.gameObject);

            if (gameObject.CompareTag("Medium_Asteroid") || gameObject.CompareTag("Large_Asteroid")) // спаун 2х малых астероидов
            {                                                                                        // из среднего/крупного
                asteroidSpawnNew.CreateSmallFromLargeOrMedium(gameObject.transform);
            }

            if (gameObject.CompareTag("Large_Asteroid"))    // начисление очков за уничтожение астероида
            {
                gameLogics.GetScore(LargeAsteroidPoints);   // отправка очков в игрокую логику
                gameLogics.IncrementDestroyedAsteroidCount(); // увеличение числа уничтоженных астероидов 
                AudioSource.PlayClipAtPoint(AudioClips[0], gameObject.transform.position);  // проигрывание звука уничтожения крупного астероида
            }

            else if (gameObject.CompareTag("Medium_Asteroid"))
            {
                gameLogics.GetScore(MediumAsteroidPoints);
                gameLogics.IncrementDestroyedAsteroidCount();
                AudioSource.PlayClipAtPoint(AudioClips[1], gameObject.transform.position);
            }

            else if (gameObject.CompareTag("Small_Asteroid"))
            {
                gameLogics.GetScore(SmallAsteroidPoints);
                gameLogics.IncrementDestroyedAsteroidCount();                
                AudioSource.PlayClipAtPoint(AudioClips[2], gameObject.transform.position);
            }
            audioSource.Play();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ufo") || other.CompareTag("UfoBullet"))                           // если столкновение с НЛО
        {                                                                                            // или пулей НЛО
            if (gameObject.CompareTag("Medium_Asteroid") || gameObject.CompareTag("Large_Asteroid")) // спаун 2х малых астероидов
            {                                                                                        // из среднего/крупного
                asteroidSpawnNew.CreateSmallFromLargeOrMedium(gameObject.transform);
            }
            gameLogics.IncrementDestroyedAsteroidCount();
            Destroy(gameObject);
        }
            
    }

    
}
