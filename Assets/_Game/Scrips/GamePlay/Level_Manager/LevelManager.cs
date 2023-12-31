﻿using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;
using JetBrains.Annotations;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{

    public Weapontype newCurrentWeaponType;
    public FixedJoystick _joystick;
    public PlayerController player; 
    public  Bot bot;
    public List<Bot> botList = new List<Bot>();
    public int numberOfBots = 2;
    public Transform spawnPosition;
    public float maxRadius=50;
    protected WeaponData weaponData;
    public int endPoint = 0;

    private void Awake()
    {
        DataManager.Instance.Init();
        player.isDead= false;
        SpawnPlayer();
    }
   

    public void SpawnPlayer()
    {
        player = LeanPool.Spawn(player,spawnPosition);

        if (player.weaponSpawn == null)
        {
            player.OnInit();
            Debug.Log("da thay doi");
        }
      //  player.Init();
        //  player=Instantiate(player,spawnPosition);

    }
    public void SpawnBot()
    {
       
        if (bot == null)
        {
          
            return;
        }
        for (int i=0;i<numberOfBots;i++)
        {
            Vector3 randomPositison = NavMeshUtil.GetRandomPoint(spawnPosition.position,maxRadius);
         
          //  Bot newbot = Instantiate(bot,randomPositison,Quaternion.identity);
            Bot newbot = LeanPool.Spawn(bot, randomPositison, Quaternion.identity);
            botList.Add(newbot);
          
        } 
    }
   public void BotDeath(Bot bot)
    {
        botList.Remove(bot);
    }


}
