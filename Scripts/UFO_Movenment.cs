using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO_Movenment : MonoBehaviour
{
    public GameObject Bullet;           // для префаба пули
    public AudioClip[] AudioClips;      // массив звуков
    public float UFO_Speed;             // скорость НЛО
    public float UfoFireRate;           // частота стрельбы НЛО
    public float UfoHealthPoints;       // кол-во жизней НЛО
    public int UfoPoints;               // очки за уничтожение НЛО

    private GameLogics gameLogics;      // для передачи данных о наборе очков          
    private Transform ufoGun;           // для доступа к "пушке" НЛО
    private AudioSource audioSource;    // для проигрывания звуков 
    private float nextBullet;           // для расчёта времени выстрела следующей пули

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     // если столкновение с игроком, то телепор игрока в точку респауна, проигрывание звука
        {                                   // и уничтожение НЛО с присвоением очков игроку
            AudioSource.PlayClipAtPoint(AudioClips[1], gameObject.transform.position);
            gameLogics.GetScore(UfoPoints);
            other.gameObject.transform.position = Vector3.zero;
            Destroy(gameObject);
        }
        else if (other.tag.Contains("Asteroid"))    // столкновение с астероидом, проигрывание звука, уничтожение обоих объектов
        {
            AudioSource.PlayClipAtPoint(AudioClips[1], gameObject.transform.position);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if (other.CompareTag("Bullet"))         // столкновение с пулей игрока
        {
            Destroy(other.gameObject);
            UfoHealthPoints--;                  // отнимание ХП НЛО
            if (UfoHealthPoints == 0)           // если хп = 0, то присвоение очков игроку, проигрывание звука и уничтожение НЛО
            {
                AudioSource.PlayClipAtPoint(AudioClips[1], gameObject.transform.position);
                gameLogics.GetScore(UfoPoints);
                Destroy(gameObject);
            }            
        }
    }
    private void Start()
    {
        Rigidbody Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = RandomLeftRightMove();                                     // случайное движение НЛО влево/вправо
        ufoGun = GetComponentInChildren<Transform>();                                   // пушка НЛО
        GameObject gameLogicsObject = GameObject.FindGameObjectWithTag("GameLogics");   // находим объект, отвечающий за логику
        gameLogics = gameLogicsObject.GetComponent<GameLogics>();                   
        audioSource = GetComponent<AudioSource>();                                      // для проигрывания звуков
        

    }

    private void Update()
    {
        if (Time.time > nextBullet)                 // стрельба НЛО, проигрывание звука стрельбы
        {
            nextBullet = Time.time + UfoFireRate;
                        
            Instantiate(Bullet, ufoGun.position, Quaternion.identity);
            audioSource.clip = AudioClips[0];
            audioSource.Play();
        }
    }
    /// <summary>
    /// Метод, задающий скорость и направление движения НЛО по оси X случайным образом
    /// </summary>
    /// <returns>Вектор движения по оси X</returns>
    private Vector3 RandomLeftRightMove()
    {
        float [] xVelocity = new float[] {-1f, -0.5f, 0.5f, 1f };
        Vector3 vector3 = new Vector3(xVelocity[Mathf.RoundToInt(Random.Range(0,4))], 0, 0) * UFO_Speed;
        return vector3;
    }    
}
