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

    private Transform target;

    public void SeekDirec(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
        this.direction = direction;
        StartCoroutine(SelfDestruct());
    }
    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.7f);
        LeanPool.Despawn(gameObject);
    }
    public void OnInIt()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstString.Bot) || other.CompareTag(ConstString.Player))
        {
            if(other.name != ShooterName)
            {
                LeanPool.Despawn(gameObject);
                Character character = other.GetComponent<Character>();
                character.OnDead();
                StopAllCoroutines();

               
            }
        }
      
    }





}
