using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;




public class Character : MonoBehaviour
{
    [Header("Move Infor")]
    [SerializeField] public Rigidbody _rigibody;
    [SerializeField] public FixedJoystick _joystick;
    [SerializeField] public Animator _animator;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;

    

    [Header("Collier Info")]
    [SerializeField] public LayerMask botLayerMark;
    [SerializeField] public float circleRadius;
  
    public Transform nearEnemy;
    public Vector3 direc;
    protected WeaponData weaponData;
    protected Bullet bullet;
    public string currentAnimName;
    [Header("Weapon Info")]
    [SerializeField] public Transform holdWeapon;
    [SerializeField] public float _moveSpeed;
    [SerializeField] public Weapontype currentWeaponType;
    //  [SerializeField] public GameObject weapon;
    //  [SerializeField] public GameObject bulletPrefab;
    public Transform firePos;
  
    protected virtual void Start()
    {
      //  Instantiate(weapon, holdWeapon);

    }
    protected virtual void Update()
    {
       // Debug.Log(circleRadius);
        if (isDead)
        {
            return;
        }
        CheckBoxBot();
        if (isIdle && !isAttack && nearEnemy != null)
        {
            isAttack = true;
            AttackBot();
            _animator.SetBool(ConstString.is_Attack, true);
            Invoke(nameof(ResetAttack), 2f);
          
        }
        
    }
    public void AttackBot()
    {

        direc = nearEnemy.position - transform.position;
        Bullet spawnBullet = LeanPool.Spawn(weaponData.bullet,firePos.position,firePos.rotation);
        spawnBullet.transform.rotation= Quaternion.Euler(-90,0,0);
      //  GameObject spawnBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        spawnBullet.GetComponent<Bullet>().ShooterName=gameObject.name;
        Bullet bulletOjb = spawnBullet.GetComponent<Bullet>();
        bulletOjb.SeekDirec(direc);
        holdWeapon.gameObject.SetActive(false);

        
    }
    
  
    
    public void OnDead()
    {
        isDead = true;
        isIdle = false;
        _animator.SetBool(ConstString.is_Dead, true);
        int defaultLayer = LayerMask.NameToLayer(ConstString.Default_Layer);
        gameObject.layer = defaultLayer;
        Debug.Log("trung dan");
        Invoke(nameof(DestroyGameObject),2f);
    }
    public void DestroyGameObject() {
            
        Destroy(gameObject);
    }

    private void CheckBoxBot()
    {

        Collider[] bot = Physics.OverlapSphere(transform.position, circleRadius, botLayerMark);
        float miniumDistance = Mathf.Infinity;
        if (bot.Length >1)
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

    

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
    public void SpawnWeapon()
    {
        Instantiate(weaponData.weapon,holdWeapon);
    }
}

