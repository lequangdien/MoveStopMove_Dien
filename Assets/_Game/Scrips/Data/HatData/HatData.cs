using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HatType
{
    hat = 0,
    hat_1 = 1,
    hat_2 = 2,
    hat_3 = 3,

}
[Serializable]
public class HatData 
{
    public HatType HatType;
    public Sprite sprite;
    public GameObject hatPrefab;
}
