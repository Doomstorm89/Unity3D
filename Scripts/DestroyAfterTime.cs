using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float BulletDestroyDelay = 2.0f;  // Задержка перед удалением объекта
    /// <summary>
    /// Удаление пуль игрока и НЛО после определённого времени
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("UfoBullet")) Destroy(other.gameObject, BulletDestroyDelay);        
    }
   
        
}
