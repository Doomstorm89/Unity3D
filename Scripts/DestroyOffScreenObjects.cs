using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreenObjects : MonoBehaviour
{
    /// <summary>
    /// Удаление объектов, вылетевших за пределы игрового поля
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
