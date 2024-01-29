using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkin : Singleton<ShopSkin>
{
    public void shopSKin()
    {
        UiManager.Instance.mainMenu.SetActive(false);
        UiManager.Instance.weaponSkinOj.SetActive(true);
        UiManager.Instance.SetImage();
    }
}
  

