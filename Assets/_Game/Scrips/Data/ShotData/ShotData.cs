using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ShotType
{
    Shot = 0,
    Shot_1 = 1,
    Shot_2 = 2,
    Shot_3 = 3,

}
[Serializable]
public class ShotData 
{
    ShotType ShotType;
    public Sprite sprite;
    public Material shotMaterial;
}
