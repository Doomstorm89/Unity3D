using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{    
    public float BulletSpeed;  // скорость полёта пули
    
    private Rigidbody Rigidbody;
            
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();          
        Rigidbody.velocity = Rigidbody.transform.up * BulletSpeed;        
    }

    

}
