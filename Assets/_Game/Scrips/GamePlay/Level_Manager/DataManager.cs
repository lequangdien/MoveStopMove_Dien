using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO WeaponDataSO;
    public List<WeaponData> listWeaponItemData;
    public Weapontype listWeapontype;
    public UseData useData;
    public HatDataSO HatDataSO;
    public List<HatData> listHatData;

    private void Start()
    {
        listWeaponItemData = WeaponDataSO.weaponListData;
        listHatData = HatDataSO.hotListData;
        //PlayerPrefs.DeleteKey(ConstString.PLAYERPREFKEY);
    }
    public void Init()
    {
        useData = GetPlayerPref();
    }
    public void ChangeWeaponData(Weapontype weapontype)
    {
        useData.weaponTypeData = weapontype;
        SavePlayePref(useData);
        Debug.Log("da chay vao day");
    }

    public void SavePlayePref(UseData useData)
    {
        string useDataString = JsonUtility.ToJson(useData);
        PlayerPrefs.SetString(ConstString.PLAYERPREFKEY, useDataString);
    }
    public UseData GetPlayerPref()
    {
        string UseDataString = PlayerPrefs.GetString(ConstString.PLAYERPREFKEY);
        UseData useData = JsonUtility.FromJson<UseData>(UseDataString);
        return useData;
    }

    public WeaponData GetWeaponData(Weapontype weapontype)
    {
        List<WeaponData> weapons = WeaponDataSO.weaponListData;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapontype == weapons[i].weaponType)
            {
                return weapons[i];
            }
        }
        return null;
    }
   
}
