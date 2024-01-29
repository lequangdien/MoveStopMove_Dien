using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    private float speed = -100f;

    void Update()
    {
        transform.Rotate(0f, Time.deltaTime * speed,0f);
    }
}
