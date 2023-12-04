using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;
using Lean.Pool;
using JetBrains.Annotations;

public class LevelManager : Singleton<LevelManager>
{
   
    public FixedJoystick _joystick;
    public PlayerController player;
    
    public  Bot bot;
    public List<Bot> botList = new List<Bot>();
    public int numberOfBots = 2;
    public Transform spawnPosition;
    public float maxRadius=50;
    public int theodoi = 0;
    
        
    public void Awake()
    {
      SpawnPlayer();
      SpawnBot();
      Time.timeScale = 0;

    }
    
  
    private void SpawnPlayer()
    {
        player = LeanPool.Spawn(player,spawnPosition);
      //  player=Instantiate(player,spawnPosition);
        player.OnInit();
    }
    public void SpawnBot()
    {
       
        if (bot == null)
        {
            Debug.Log("code chay1");
            return;
        }
        for (int i=0;i<numberOfBots;i++)
        {
            Vector3 randomPositison = NavMeshUtil.GetRandomPoint(spawnPosition.position,maxRadius);
            Debug.Log("code chay");
          //  Bot newbot = Instantiate(bot,randomPositison,Quaternion.identity);
            Bot newbot = LeanPool.Spawn(bot, randomPositison, Quaternion.identity);
            botList.Add(newbot);
            theodoi++;
        }
        Debug.Log(theodoi);
        if (botList.Count ==0)
        {
            Debug.Log("win game");
        }
    }

}
