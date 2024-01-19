using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : Bullet
{
    private float speedBullet = -1000f;
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * speedBullet);
    }
}
