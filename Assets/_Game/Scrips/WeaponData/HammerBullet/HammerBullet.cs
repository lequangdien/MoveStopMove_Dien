using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBullet : Bullet
{
    private float speed = -1000f;

    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * speed);
    }
}
