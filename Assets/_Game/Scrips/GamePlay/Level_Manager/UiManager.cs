using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UiManager : Singleton<UiManager>
{

  
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject _joystick;
    [SerializeField] public Button startGame;
    [SerializeField] public Button weaponShop;
    [SerializeField] public GameObject weaponObjectHammer;
    [SerializeField] public GameObject weaponObjectAxe;
    [SerializeField] public Transform mainCamera;
    [SerializeField] public GameObject uiPlayerIsDead;
    [SerializeField] public Button backToMainMenu;
    [SerializeField] public GameState gamestate = GameState.UNPLAY;
    [SerializeField] public Transform point;
    [SerializeField] public Button selectWeapon;
    [SerializeField] public Button exit;
    [SerializeField] public Button nextShopWeapon;
    [SerializeField] public Button backShopWeapon;
    [SerializeField] public GameObject selectWeaponOj;
    [SerializeField] public GameObject unSelectWeaponOj;
  
    private Weapontype currentWeaponType;
    private Weapon weapon;
    private int index;
    private void Start()
    {
        index = 0;
        startGame.onClick.AddListener(TurnOffMainMenu);
        weaponShop.onClick.AddListener(ShopWeapon);
        backToMainMenu.onClick.AddListener(UiYouDead);
        selectWeapon.onClick.AddListener(SelectWeapon);
        exit.onClick.AddListener(Exit);
        nextShopWeapon.onClick.AddListener(NextWeapon);
        backShopWeapon.onClick.AddListener(BackWeapon);
        
 
    }
    private void Update()
    {
        if (LevelManager.Instance.player.isDead == true)
        {
            gamestate= GameState.UNPLAY;
            uiPlayerIsDead.SetActive(true);
            _joystick.SetActive(false);
        }
        
        
    }
   
    public void TurnOffMainMenu()
    {
        LevelManager.Instance.SpawnBot();
        gamestate = GameState.PLAY;
        mainMenu.SetActive(false);
        _joystick.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        weaponObjectHammer.SetActive(false);
        DestroyWeapon(weapon);
       
    }
   
    public void ShopWeapon()
    {
        mainMenu.SetActive(false);
        weaponObjectHammer.SetActive(true);
        LoadWeapon(index);
       
    }
    public void LoadWeapon(int index)
    {
        weapon = Instantiate(DataManager.Instance.WeaponDataSO.weaponListData[index].weapon, point.transform);
        weapon.gameObject.transform.localScale += new Vector3(weapon.transform.localScale.x*4,weapon.transform.localScale.y*4,weapon.transform.localScale.z*4);
        //weapon.gameObject.transform.rotation= Quaternion.Euler(0,90,0);
    }
    public void DestroyWeapon(Weapon weapon)
    {
        Destroy(weapon.gameObject);
    }
    public void SelectWeapon()
    {
            LevelManager.Instance.player.ChangeWeapon((Weapontype)index);
            DataManager.Instance.ChangeWeaponData(LevelManager.Instance.player.WeaponData.weaponType);
            Debug.Log("da luu");
            TurnEquipped();
    }
    
   
    public void NextWeapon()
    {
        if (index < DataManager.Instance.listWeaponItemData.Count -1)
        {
            index++;
            DestroyWeapon(weapon);
            LoadWeapon(index);
           
            if (index==(int)LevelManager.Instance.player.WeaponData.weaponType)
            {
                TurnEquipped();
            }
            else
            {
                TurnSelect();
            }
        }
    }
    public void BackWeapon()
    {
        if (index >0)
        {
            index--;
            DestroyWeapon(weapon);
            LoadWeapon(index);
            
            if (index==(int)LevelManager.Instance.player.WeaponData.weaponType)
            {
                TurnEquipped();
            }
            else
            {
                TurnSelect();
            }
        }
    }
    public void UiYouDead()
    {
        LevelManager.Instance.player.isDead=false;
        uiPlayerIsDead.SetActive(false);
        SceneManager.LoadScene(ConstString.SCENE);
    }
    public  void TurnSelect()
    {
        selectWeaponOj.SetActive(true);
        unSelectWeaponOj.SetActive(false);
    }
    public void TurnEquipped()
    {
        selectWeaponOj.SetActive(false);
        unSelectWeaponOj.SetActive(true);
    }
    public enum GameState
    {
        PLAY,
        UNPLAY
    }
   

   
}
