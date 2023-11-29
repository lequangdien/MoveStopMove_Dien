using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO WeaponDataSO;
    
    public WeaponData GetWeaponData(Weapontype weapontype)
    {
        List<WeaponData> weapons = WeaponDataSO.weaponListData;
        for (int i=0;i<weapons.Count;i++)
        {
            if (weapontype == weapons[i].weaponType)
            {
                return weapons[i];
            }
        }
        return null;
    }
}
