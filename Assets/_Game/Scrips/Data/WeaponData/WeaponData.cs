using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum Weapontype
    {
        Hammer=0,
        Axe=1,
        Boomerang=2,
    }
    [Serializable]
    public class WeaponData
    {
        public Weapontype weaponType;
        public Bullet bullet;
        public Weapon weapon;
        
       // public float range;
    }

