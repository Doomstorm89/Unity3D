using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    /// <summary>
    /// Телепортация объектов, достигающих верхнего или нижнего краев игрового поля
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        float zTemporary = other.transform.position.z * -1f; // временная переменная, для хранения "обратной" Z

        if (zTemporary < 0)                           // проверка и сдвиг Z на 1f, чтобы избежать бесконечной телепортации
            zTemporary += 1f;

        else
            zTemporary -= 1f;

        other.transform.position = new Vector3(       // присвоение нового вектора, объект телепортирован к противоположной границе
            other.transform.position.x,
            other.transform.position.y,
            zTemporary);

    }
}