using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [Header("Move Infor")]
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;
    //   [SerializeField] private LayerMask groundLayer;

    [Header("Collier Info")]
    [SerializeField] private LayerMask botLayerMark;
    [SerializeField] private float circleRadius;
    public Transform target;
    public Transform nearEnemy;
    public Vector3 direc;

    [Header("Weapon Info")]
    [SerializeField] private Transform holdWeapon;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject bulletPrefab;
    public Transform firePos;
    //[SerializeField] private Transform firePos;

    [SerializeField] private float _moveSpeed;
    public string currentAnimName;
    //public float attackDuration = 5.0f;
    //private IEnumerator attackCoroutine;
    
    private void Start()
    {
        Instantiate(weapon,holdWeapon);
    }
    private void FixedUpdate()
    {
      CheckBoxBot();
        if (isIdle && !isAttack && nearEnemy !=null)
        {
            isAttack = true;
            AttackBot();
            _animator.SetBool(ConstString.is_Attack, true);
            Invoke(nameof(ResetAttack),2f);
        }                  
    }

    private void Update()
    {
        _rigibody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigibody.velocity.y, _joystick.Vertical * _moveSpeed);
        if(_joystick.Horizontal !=0 || _joystick.Vertical != 0)
        {
            
            transform.rotation = Quaternion.LookRotation(_rigibody.velocity);

            
            isIdle = false;
            _animator.SetBool(ConstString.is_Idle,isIdle);
            _animator.SetBool(ConstString.is_Attack, false);
        }

        else
        {
           
           isIdle = true;
            _animator.SetBool(ConstString.is_Idle, isIdle);
        }

    }
    public void AttackBot()
    {
        
            direc = nearEnemy.position - transform.position;
            GameObject spawnBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            Bullet bulletOjb = spawnBullet.GetComponent<Bullet>();
            bulletOjb.SeekDirec(direc);
            holdWeapon.gameObject.SetActive(false);

     
    }
 
    private void CheckBoxBot()
    {
       
        Collider[] bot = Physics.OverlapSphere(transform.position, circleRadius, botLayerMark);
        float miniumDistance = Mathf.Infinity;
        if (bot.Length != 0)
        {
            foreach (Collider collider in bot)
            {
                if (collider.gameObject != this.gameObject)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < miniumDistance)
                    {
                        miniumDistance = distance;
                        nearEnemy = collider.transform;
                    }
                }

            }
            //Facing enemy if player found them
            if (isIdle)
            {
                transform.LookAt(nearEnemy);
            }
        }
        else
        {
            nearEnemy = null;
        }
    }
    public void ResetAttack()
    {
        isAttack = false;
        holdWeapon.gameObject.SetActive(true);
        _animator.SetBool(ConstString.is_Attack, false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
   
  




}
