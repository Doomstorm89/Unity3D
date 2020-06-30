using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UFO : MonoBehaviour
{
    public GameObject UFO_prefab;   // префаб НЛО
    public float TimeToSpawn;       // время до и между спауном НЛО

    private float nextUfoSpawn;     // время спауна следующего НЛО
            
    void Start()
    {
        nextUfoSpawn = TimeToSpawn;
    }
        
    void Update()
    {
        CreateUfo();
    }        
    /// <summary>
    /// Метод, создающий НЛО в случайном месте по вертикали
    /// </summary>
    public void CreateUfo()
    {        
        if (Time.timeSinceLevelLoad > nextUfoSpawn)
        
        {
            float [] xPositions = new float[] { -22f, 22f };            

            nextUfoSpawn = Time.timeSinceLevelLoad + TimeToSpawn;

            Instantiate(UFO_prefab, new Vector3(
                xPositions[Mathf.RoundToInt(Random.Range(0, 1))],
                0,
                Random.Range(-11f, 11f)), Quaternion.identity);
        }
    }

    
}
