using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerController : Character
{

    private GameObject currentHat;
    private FixedJoystick _joystick;
    private GameObject currentShot;

    public Renderer renderPlayer;
    public Material materialPlayer;
    public Transform playerPosition;

    public Vector3 movement;
    public void Start()
    {
        _joystick = LevelManager.Instance._joystick;
        currentWeaponType = Weapontype.Hammer;
        renderPlayer.material = materialPlayer;
        SpawnWeapon();
    }


    private void FixedUpdate()
    {
        OnMove();
        if (isDead)
        {
            return;
        }
    }

    public void OnInit()
    {

        if (weaponData == null)
        {
            currentWeaponType = DataManager.Instance.GetPlayerPref().weaponTypeData;
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }

    }

    public void ChangeWeapon(Weapontype weapontype)
    {

        weaponData = DataManager.Instance.listWeaponItemData[(int)weapontype];
        Destroy(this.weaponSpawn.gameObject);
        weaponSpawn = Instantiate(weaponData.weapon, holdWeapon);
    }
    public void ChangeHatSkin()
    {
        if (UiManager.Instance.previousSelectedIndex != -1)
        {
            HatData selectedHatData = DataManager.Instance.HatDataSO.hotListData[UiManager.Instance.previousSelectedIndex];
            if (selectedHatData != null && selectedHatData.hatPrefab != null)
            {
                if (currentHat !=null)
                {
                    Destroy(currentHat);
                }
                  currentHat=Instantiate(selectedHatData.hatPrefab,playerPosition);
            }
            else
            {
                Debug.Log("lỗi");
            }
        }
        else
        {
            Debug.Log("lỗi tiếp");
        }
    }
    public void ChangeShop_Shot()
    {
        if (UiManager.Instance.previousSelectedIndex != -1)
        {
            ShotData shotData_Shot = DataManager.Instance.shotDataSO.Shotdata[UiManager.Instance.previousSelectedIndex];
            if (shotData_Shot != null && shotData_Shot.shotMaterial != null)
            {
                renderPlayer.material= shotData_Shot.shotMaterial;
            }
            else
            {
                Debug.Log("lỗi");
            }
        }
        else
        {
            Debug.Log("lỗi tiếp");
        }
    }
    

    public void OnMove()
    {
        _rigibody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigibody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {

            transform.rotation = Quaternion.LookRotation(_rigibody.velocity);


            isIdle = false;
            _animator.SetBool(ConstString.IS_IDLE, isIdle);
            _animator.SetBool(ConstString.IS_ATTACK, false);


        }

        else
        {

            isIdle = true;
            _animator.SetBool(ConstString.IS_IDLE, isIdle);
        }
        if (!IsWallInFront())
        {
            // Áp dụng di chuyển nếu không có tường
            transform.Translate(movement);
        }
    }
    bool IsWallInFront()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
        {

            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}
