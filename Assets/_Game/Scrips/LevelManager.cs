using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public PlayerController player;
    private void Awake()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        player.OnInit();
    }
}
