using Lean.Pool;
using UnityEngine;




public class Character : MonoBehaviour
{
    [Header("Move Infor")]
    [SerializeField] public Rigidbody _rigibody;

    [SerializeField] public Animator _animator;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;

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
    //   public Weapon WeaponSpawn { get => weaponSpawn; set => weaponSpawn = value; }


    protected virtual void Update()
    {
        CheckBoxBot();

        if (isIdle && !isAttack && nearEnemy != null)
        {
            isAttack = true;
            AttackBot();
            _animator.SetBool(ConstString.IS_ATTACK, true);
            Invoke(nameof(ResetAttack), 2f);

        }
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

        //   Destroy(gameObject);
        LeanPool.Despawn(gameObject);
    }

    private void CheckBoxBot()
    {

        Collider[] bot = Physics.OverlapSphere(transform.position, circleRadius, botLayerMark);
        float miniumDistance = Mathf.Infinity;
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
                }
            }
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
        if (isIdle)
        {
            transform.LookAt(nearEnemy);

        }
        else
        {
            if (isIndicate)
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

