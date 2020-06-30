using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody Rigidbody;     // для прикрепления модели игрока
    public GameObject Bullet;       // модель пули
    public Transform PlayerGun;     // пушка игрока (откуда полетят пули)
    public Text GameOver;           // текст окончания игры
    public AudioClip[] AudioClips;  // массив звуков стрельбы и взрывов

    public float Speed;             // скорость игрока  
    public float MaxSpeed;          // максимальная скорость
    public float MaxAcceleration;   // максимальное ускорение
    public float RotationSpeed;     // скорость вращения
    public float FireRate;          // темп стрельбы
    public int PlayerTryCount;      // кол-во жизней/попыток игрока      
        
    private MeshRenderer[] meshRenderers;  // массив мешей игрока для добавления эффекта мерцания при неуязвимости
    private GameLogics gameLogics;  // для использования методов игровой логики
    private AudioSource audioSource;// для проигрывания звуков игрока
    private Vector3 startPosition;  // стартовая позиция игрока    
    private float nextBullet;       // расчёт следующего выстрела
    private bool spawning;          // состояние для добавления неуязвимости        
    private bool playerDead;        // для определения конца игры
               
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();                      // получаем физ. тело игрока
        Rigidbody.maxAngularVelocity = RotationSpeed;               // задаем скорость вращения        
        meshRenderers = GetComponentsInChildren<MeshRenderer>();    // получаем дочерние меши для скрытия/показа (мерцание)              
        GameObject gameLogicsObject = GameObject.FindGameObjectWithTag("GameLogics"); // получаем объект, отвечающий за игровую логику
        gameLogics = gameLogicsObject.GetComponent<GameLogics>();
        startPosition = Rigidbody.position;                         // сохраняем стартовую позицию игрока                       
        audioSource = GetComponent<AudioSource>();                  // получаем компонент для работы со звуком
        spawning = true;                                            // неуязвимость включена  
    }       

    void Update()
    {        
        StartCoroutine(PlayerRespawn());            // метод, отвечающий за мерцание и тайминг неуязвимости
        Shooting();                                 // метод, отвечающий за стрельбу
        Death();                                    // метод проверки смерти, показ текста "гейм овер", уничтожение игрока
    }

    private void FixedUpdate()
    {        
        MovingAndRotating();                        // метод движения и поворота               
    }

    private void OnTriggerEnter(Collider other)     // проверка столкновений, расчёт жизней игрока
    {
        int tmp = PlayerTryCount;                   // временная переменная для отслеживания изменения жизней
        if (other.CompareTag("Ufo") || other.tag.Contains("Asteroid") && !spawning) // проверка на НЛО, Астероид и отсутствие неуязвимости
        {
            AudioSource.PlayClipAtPoint(AudioClips[2], gameObject.transform.position);  // проигрываем звук уничтожения
            PlayerTryCount--;                                                           // убавляем жизни игрока
        }
        if (other.CompareTag("UfoBullet") && !spawning)                                 // проверка столкновения с пулей НЛО
        {
            AudioSource.PlayClipAtPoint(AudioClips[2], gameObject.transform.position);
            Destroy(other.gameObject);
            PlayerTryCount--;                       
        }
        if (tmp != PlayerTryCount && PlayerTryCount != 0 && !spawning)  // если кол-во жизней игрока изменилось, то неуязвимость
        {
            tmp = PlayerTryCount;            
            spawning = true;
            gameObject.transform.position = startPosition;                       
        }
        gameLogics.UpdateScore();                                       // обновляем счёт очков в игровой логике
    }        
    /// <summary>
    /// Метод мерцающего респауна игрока и неуязвимости на 3 секунды
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerRespawn()                                 
    {
        while (spawning)
        {            
            for (int i = 0; i < 8; i++)
            {
                foreach (var item in meshRenderers)     // выключаем и включаем меши игрового объекта игрока
                {
                    item.enabled = false;
                }
                yield return new WaitForSeconds(0.15f);
                foreach (var item in meshRenderers)
                {
                    item.enabled = true;
                }
                yield return new WaitForSeconds(0.15f);
            }            
            spawning = false;            
        }
    }       
    
    /// <summary>
    /// Метод стрельбы по нажатию на пробел, с учётом темпа стрельбы + спаун пуль и проигрывание звука стрельбы
    /// </summary>
    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeSinceLevelLoad > nextBullet)        
        {
            nextBullet = Time.timeSinceLevelLoad + FireRate;
            Instantiate(Bullet, PlayerGun.position, PlayerGun.rotation);
            audioSource.clip = AudioClips[0];
            audioSource.Play();
        }
        
    }
    /// <summary>
    /// Метод движения и вращения игрока + проигрывание звука движения вперёд
    /// </summary>
    private void MovingAndRotating()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 vector3 = new Vector3(0.0f, 0.0f, moveVertical);

        Rigidbody.transform.Rotate(0, 0, -moveHorizontal * RotationSpeed);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rigidbody.AddForce(Rigidbody.transform.up * moveVertical * Speed, ForceMode.VelocityChange);
            audioSource.clip = AudioClips[1];
            audioSource.Play();
        }
    }
    /// <summary>
    /// Метод, проверяющий умер ли игрок (жизни <=0 ), уничтожающий корабль игрока и меняющий текст на "гейм овер"
    /// </summary>
    private void Death()
    {
        if (PlayerTryCount <= 0)
        {
            playerDead = true;
            Destroy(this.gameObject);
            GameOver.text = "Game Over. \nPress any key to restart.";           
        }        
    }
    /// <summary>
    /// Метод уменьшения жизней игрока + звук уничтожения игрока
    /// </summary>
    public void DecreasePlayerTryCount()
    {
        if (PlayerTryCount > 0)
        {
            PlayerTryCount--;
            audioSource.clip = AudioClips[2];
            audioSource.Play();
        }
    }
    /// <summary>
    /// Метод для получения жизней игрока
    /// </summary>
    /// <returns>Кол-во жизней игрока (TryCount)</returns>
    public int GetPlayerTryCount()
    {
        return PlayerTryCount;
    }
    /// <summary>
    /// Метод проверки умер ли игрок. Если игрок мёртв, то в игровой логике по нажатию кнопки происходит рестарт уровня
    /// </summary>
    /// <returns>Мёртв ли игрок (жизни <=0)</returns>
    public bool IsPlayerDead()
    {
        return playerDead;
    }
   

    
}
