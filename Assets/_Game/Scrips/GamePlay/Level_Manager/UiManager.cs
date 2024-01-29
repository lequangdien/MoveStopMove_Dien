using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{


    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject _joystick;
    [SerializeField] public Button startGame;
    [SerializeField] public Button weaponShop;
    [SerializeField] public GameObject weaponShopGameObj;
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
    [SerializeField] public Button weaponShopSkin;
    [SerializeField] public GameObject weaponSkinOj;
    [SerializeField] public GameObject settingGameObject;
    [SerializeField] public Button settingButton;
    [SerializeField] public Button exitShopSkin;



    private Weapon weapon;
    private int index;

    //TestImage;
    public List<Sprite> imageSprites;
    public GameObject imagePrefab;
    public Transform parentTransform;
    public int numberOfImages = 10;
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
        weaponShopSkin.onClick.AddListener(ShopSkin.Instance.shopSKin);
        settingButton.onClick.AddListener(SettingUI);
        exitShopSkin.onClick.AddListener (ExitShopSkin);
    }
    private void Update()
    {
        if (LevelManager.Instance.player.isDead == true)
        {
            gamestate = GameState.UNPLAY;
            uiPlayerIsDead.SetActive(true);
            _joystick.SetActive(false);
        }


    }
    public void ExitShopSkin()
    {
        weaponSkinOj.SetActive(false);
        mainMenu.SetActive(true);
    }
   

    public void TurnOffMainMenu()
    {
        LevelManager.Instance.SpawnBot();
        gamestate = GameState.PLAY;
        mainMenu.SetActive(false);
        _joystick.SetActive(true);
        settingGameObject.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        weaponShopGameObj.SetActive(false);
        DestroyWeapon(weapon);

    }
    public void SettingUI()
    {

    }

    public void ShopWeapon()
    {
        mainMenu.SetActive(false);
        weaponShopGameObj.SetActive(true);
        LoadWeapon(index);

    }
    public void LoadWeapon(int index)
    {
        weapon = Instantiate(DataManager.Instance.WeaponDataSO.weaponListData[index].weapon, point.transform);
        weapon.gameObject.transform.localScale += new Vector3(weapon.transform.localScale.x * 4, weapon.transform.localScale.y * 4, weapon.transform.localScale.z * 4);
      //  weapon.transform.Rotate(Vector3.forward, Time.deltaTime *rotationSpeed);
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
        if (index < DataManager.Instance.listWeaponItemData.Count - 1)
        {
            index++;
            DestroyWeapon(weapon);
            LoadWeapon(index);

            if (index == (int)LevelManager.Instance.player.WeaponData.weaponType)
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
        if (index > 0)
        {
            index--;
            DestroyWeapon(weapon);
            LoadWeapon(index);

            if (index == (int)LevelManager.Instance.player.WeaponData.weaponType)
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
        LevelManager.Instance.player.isDead = false;
        uiPlayerIsDead.SetActive(false);
        SceneManager.LoadScene(ConstString.SCENE);
    }
    public void TurnSelect()
    {
        selectWeaponOj.SetActive(true);
        unSelectWeaponOj.SetActive(false);
    }
    public void TurnEquipped()
    {
        selectWeaponOj.SetActive(false);
        unSelectWeaponOj.SetActive(true);
    }
    public void SetImage()
    {
        for (int i=0;i<numberOfImages;i++)
        {
            GameObject imageObject = Instantiate(imagePrefab,parentTransform);
            Image imageCompoient=imageObject.GetComponent<Image>();
            if (i<imageSprites.Count)
            {
                imageCompoient.sprite= imageSprites[i];
            }
            else
            {
                Debug.Log("loi roi");
            }
        }
    }
    public enum GameState
    {
        PLAY,
        UNPLAY
    }



}
