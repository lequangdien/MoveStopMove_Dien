using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Lean.Pool;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    private Transform target;
    private Vector3 direction;

    public Character shooter;
    public float time = 1f;




    public void SeekDirec(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
        this.direction = direction;
        StartCoroutine(SelfDestruct());
    }
    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.65f);
        LeanPool.Despawn(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstString.CHARACTER))
        {
            Character character = other.GetComponent<Character>();

            if (character != shooter)
            {
                LeanPool.Despawn(gameObject);
                character.OnDead();
                StopAllCoroutines();

                shooter.gameObject.transform.localScale += new Vector3(character.transform.localScale.x * 0.04f, character.transform.localScale.y * 0.04f, character.transform.localScale.z * 0.04f);

            }
        }

    }
}
