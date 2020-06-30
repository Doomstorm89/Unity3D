using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLeftRight : MonoBehaviour
{
    /// <summary>
    /// Телепортация объектов, достигающих левого или правого краев игрового поля
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        float xTemporary = other.transform.position.x * -1f;

        if (xTemporary < 0)
            xTemporary += 1f;

        else
            xTemporary -= 1f;

        other.transform.position = new Vector3(
            xTemporary,
            other.transform.position.y,
            other.transform.position.z);
    }
    

}
