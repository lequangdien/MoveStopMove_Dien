using Lean.Pool;
using System.Linq;
using UnityEngine;




public class Character : MonoBehaviour
{
    [Header("Move Infor")]
    [SerializeField] public Rigidbody _rigibody;

    [SerializeField] public Animator _animator;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;
    public bool isWin = false;
    [Header("Collier Info")]
    [SerializeField] public LayerMask botLayerMark;
    [SerializeField] public float circleRadius;


    [Header("Weapon Info")]
    [SerializeField] public Transform holdWeapon;
    [SerializeField] public float _moveSpeed;
    [SerializeField] public Weapontype currentWeaponType;
    protected WeaponData weaponData;
    protected Bullet bullet;
    public Transform nearEnemy;
    public Vector3 direc;
    public string currentAnimName;
    public Vector3 scale = new Vector3(1, 1, 1);
    public Transform firePos;
    public bool isIndicate = false;
    public Weapon weaponSpawn;

    public WeaponData WeaponData { get => weaponData; set => weaponData = value; }


    protected virtual void Update()
    {
        CheckBoxBot();
        CheckStopIdle();
        RestTarget();
    }
    public void CheckStopIdle()
    {
        if (isIdle && !isAttack && nearEnemy != null)
        {
            isAttack = true;
            _animator.SetBool(ConstString.IS_ATTACK, true);
            AttackBot();
            Invoke(nameof(ResetAttack), 2f);
        }
    }
    public void RestTarget()
    {
        nearEnemy = null;
    }

    public void AttackBot()
    {
        direc = nearEnemy.position - transform.position;
        Bullet spawnBullet = LeanPool.Spawn(weaponData.bullet, firePos.position, firePos.rotation);
        spawnBullet.transform.rotation = Quaternion.Euler(-90, 0, 0);
        spawnBullet.shooter = this;
        spawnBullet.SeekDirec(direc);
        holdWeapon.gameObject.SetActive(false);
    }



    public void OnDead()
    {
        isDead = true;
        isIdle = false;
        _animator.SetBool(ConstString.IS_DEAD, true);
        int defaultLayer = LayerMask.NameToLayer(ConstString.DEFAULT_LAYER);
        gameObject.layer = defaultLayer;
        Debug.Log("trung dan");
        Invoke(nameof(DestroyGameObject), 0.5f);


    }
    public void DestroyGameObject()
    {

        LeanPool.Despawn(gameObject);
    }

    private void CheckBoxBot()
    {

        Collider[] bot = Physics.OverlapSphere(transform.position, circleRadius, botLayerMark);
        float miniumDistance = Mathf.Infinity;
        bool isAnyBotInRange = false;
        if (bot.Length > 1)
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
                    isAnyBotInRange=true;
                }
            }

        }
        if (isAnyBotInRange)
        {
            if (isIdle)
            {
                transform.LookAt(nearEnemy);
                if (!isIndicate)
                {
                    Bot botComponet = nearEnemy.GetComponent<Bot>();
                    if (botComponet != null)
                    {
                        botComponet.indicate.SetActive(true);
                        isIndicate = true;
                    }
                }
            }
        }
        else
        {
            if (isIndicate && nearEnemy != null)
            {
                Bot botComponet = nearEnemy.GetComponent<Bot>();
                if (botComponet != null)
                {
                    botComponet.indicate.SetActive(false);
                    isIndicate = false;
                }
            }
            nearEnemy = null;
        }
    }

    public void ResetAttack()
    {
        isAttack = false;
        holdWeapon.gameObject.SetActive(true);
        _animator.SetBool(ConstString.IS_ATTACK, false);
    }



    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
    public void SpawnWeapon()
    {
        weaponSpawn = Instantiate(weaponData.weapon, holdWeapon);
    }
}

