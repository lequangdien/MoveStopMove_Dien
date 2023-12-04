using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject _joystick;
    [SerializeField] public Button startGame;

    private void Start()
    {
        startGame.onClick.AddListener(TurnOffMainMenu);
    }
    public void TurnOffMainMenu()
    {
        mainMenu.SetActive(false);
        _joystick.SetActive(true);
        Time.timeScale = 1f;
    }
}
