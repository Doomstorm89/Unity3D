using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float AsteroidRotation = 3f;     // вращение астероидов
    void Start()                            // придание случайного направления и вращения астероидам
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.angularVelocity = Random.insideUnitSphere * AsteroidRotation;     
        rigidbody.velocity = new Vector3(Random.Range(-5f,5f), 0, Random.Range(-5f,5f)) * AsteroidRotation;        
    }

    
}
