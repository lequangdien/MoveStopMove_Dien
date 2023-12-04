using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class PlayerController : Character
{
    private FixedJoystick _joystick;
    private HammerWeapon hammerWeapon;
    private PlayerController player;

    public Vector3 movement;


    protected override void Start()
    {
        _joystick = LevelManager.Instance._joystick;
        currentWeaponType = Weapontype.hammer;
        SpawnWeapon();
        base.Start();
    }
  
    public void OnInit()
    {
        if (weaponData ==null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }
         
    }
    private void FixedUpdate()
    {
        OnMove();
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
    // public void CheckIndicate()
    //{
        
    //    Bot botComponent=nearEnemy.GetComponent<Bot>();
    //    if (nearEnemy != null)
    //    {
    //       if (botComponent != null)
    //        {
    //            botComponent.indicate.SetActive(true);
    //        }
    //    }
        

    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(ConstString.CHARACTER)) {
    //        other.GetComponent<Bot>().indicate.SetActive(true);
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(ConstString.CHARACTER)) {
    //        other.GetComponent<Bot>().indicate.SetActive(false);
    //    }
    //}
   









}
