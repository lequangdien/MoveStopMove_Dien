using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum Weapontype
    {
        hammer,
        knife,
    }
    [Serializable]
    public class WeaponData
    {
        public Weapontype weaponType;
        public Bullet bullet;
        public Weapon weapon;
       // public float range;
    }

