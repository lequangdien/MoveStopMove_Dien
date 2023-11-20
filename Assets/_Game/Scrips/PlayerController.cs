using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class PlayerController : Character
{
     
 
    private void FixedUpdate()
    {
        _rigibody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigibody.velocity.y, _joystick.Vertical * _moveSpeed);
      
        if (_joystick.Horizontal !=0 || _joystick.Vertical != 0)
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
   

   
   
  




}
