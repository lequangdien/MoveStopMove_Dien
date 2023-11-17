using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private Vector3 direction;

    private void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(ConstString.ENEMY_TAG) || other.CompareTag(ConstString.PLAYER_TAG))
    //    {
    //        Character character = other.GetComponent<Character>();
    //        character.IsDead();

    //    }
    //}



}
