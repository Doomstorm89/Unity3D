using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float BulletSpeed;       // скорость полёта пули

    private Rigidbody Rigidbody;    
    private Player target;          // для вычисления позиции игрока
    private Vector3 direction;      // для вычисления направления стрельбы по игроку
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();                 
        target = FindObjectOfType<Player>();

        if (target != null) // если игрок на поле, то НЛО стреляет по нему
        {
            direction = (target.transform.position - transform.position).normalized * BulletSpeed;
            Rigidbody.velocity = new Vector3(direction.x, 0, direction.z);
        }
        else               // в противном случае, НЛО стреяет по прямой
        {
            Rigidbody.velocity = Rigidbody.transform.forward * BulletSpeed;
        }
        
    }

   
}
