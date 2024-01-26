using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : Bullet
{
    private float speedBullet = -1000f;
    private Vector3 newPoint;
   
    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * speedBullet);
    }
    //protected override void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(ConstString.BOT))
    //    {
    //        base.OnTriggerEnter(other);
    //    }
    //    else
    //    {
    //        ReturnToInitialPosition();
    //    }
    //}
    private void ReturnToInitialPosition()
    {
        float distance = Vector3.Distance(transform.position, newPoint);
        float timeReturn = 0.1f;
        if (distance > timeReturn)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPoint, timeReturn * Time.deltaTime);

        }
        else
        {
            transform.position = newPoint;

        }

    }


}
