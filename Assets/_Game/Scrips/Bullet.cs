using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    public string ShooterName;
    public float time=1f;
    private Vector3 direction;

    private void Update()
    {
        rb.velocity = direction.normalized * speed;
   
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }
    public void Swpan()
    {
     //   Destroy(gameObject);
       LeanPool.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstString.Bot) || other.CompareTag(ConstString.Player))
        {
            if(other.name != ShooterName)
            {
                Destroy(gameObject);
                Character character = other.GetComponent<Character>();
                character.IsDead();
                Swpan();
               
            }
        }
    }





}
