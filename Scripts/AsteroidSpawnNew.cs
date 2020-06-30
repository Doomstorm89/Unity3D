using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnNew : MonoBehaviour
{
    public GameObject Asteroid;         // объект астероида
    
    public float LargeAsteroidMinSize;  // минимальные и максимальные размеры астероидов (крупного, среднего, малого)
    public float LargeAsteroidMaxSize;
    public float MediumAsteroidMinSize;
    public float MediumAsteroidMaxSize;
    public float SmallAsteroidMinSize;
    public float SmallAsteroidMaxSize;

    private GameLogics gameLogics;  // для доступа к игровой логике     

    private void Start()
    {
        GameObject gameLogicsObject = GameObject.FindGameObjectWithTag("GameLogics");   // получение компонента, отвечающего
        gameLogics = gameLogicsObject.GetComponent<GameLogics>();                       // за игровую логику       
        
    }
    /// <summary>
    /// Метод, возвращающий размер астероида в пределах от минимального до максимального
    /// </summary>
    /// <param name="minSize">Минимальный размер астероида</param>
    /// <param name="maxSize">Максимальный размер астероида</param>
    /// <returns>Размер астероида в пределах от мин. до макс.</returns>
    private Vector3 RandomLocalScale(float minSize, float maxSize) 
    {
        Vector3 scale = new Vector3(
            Random.Range(minSize, maxSize), 
            Random.Range(minSize, maxSize), 
            Random.Range(minSize, maxSize));
        return scale;
    }
    /// <summary>
    /// Метод, возвращий вектор для создания астероида в случайной позиции
    /// </summary>
    /// <returns>Вектор в заданных диапазонах игрового поля. Y = 0</returns>
    private Vector3 RandomPosition()
    {
        Vector3 random = new Vector3(
            Random.Range(-22f, 22f),
            0,
            Random.Range(-12f, 12f));
        return random;
    }
    /// <summary>
    /// Метод создания астероида случайного размера (от минимального малого до максимального крупного)
    /// </summary>
    public void CreateRandomAsteroid()
    {
        switch (Random.Range(0,3))
        {
            case 0: CreateLargeAsteroid(); break;
            case 1: CreateMediumAsteroid(); break;
            case 2: CreateSmallAsteroid(); break;               
        }       

    }
    /// <summary>
    /// Метод, создающий малый астероид и увеличивающий число заспауненных астероидов в игровой логике
    /// </summary>
    public void CreateSmallAsteroid()
    {
        Asteroid.transform.localScale = RandomLocalScale(SmallAsteroidMinSize, SmallAsteroidMaxSize);
        Asteroid.tag = "Small_Asteroid";
        Instantiate(Asteroid, RandomPosition(), Quaternion.identity);
        gameLogics.IncrementAsteroidsCount();
    }
    /// <summary>
    /// Метод, создающий 2 малых астероида из крупного или среднего
    /// </summary>
    /// <param name="transform"></param>
    public void CreateSmallFromLargeOrMedium(Transform transform)
    {
        Asteroid.transform.localScale = RandomLocalScale(SmallAsteroidMinSize, SmallAsteroidMaxSize);
        Asteroid.tag = "Small_Asteroid";
        transform.rotation = Quaternion.identity;
        Instantiate(Asteroid, transform.position, transform.rotation);
        Instantiate(Asteroid, transform.position, transform.rotation);
    }
    /// <summary>
    /// Метод, создающий средний астероид
    /// </summary>
    public void CreateMediumAsteroid()
    {
        Asteroid.transform.localScale = RandomLocalScale(MediumAsteroidMinSize, MediumAsteroidMaxSize);
        Asteroid.tag = "Medium_Asteroid";
        Instantiate(Asteroid, RandomPosition(), Quaternion.identity);        
        gameLogics.IncrementAsteroidsCountBy(3);
    }
    /// <summary>
    /// Метод, создающий астероид крупного размера
    /// </summary>
    public void CreateLargeAsteroid()
    {
        Asteroid.transform.localScale = RandomLocalScale(LargeAsteroidMinSize, LargeAsteroidMaxSize);
        Asteroid.tag = "Large_Asteroid";
        Instantiate(Asteroid, RandomPosition(), Quaternion.identity);        
        gameLogics.IncrementAsteroidsCountBy(3);
    }
}
