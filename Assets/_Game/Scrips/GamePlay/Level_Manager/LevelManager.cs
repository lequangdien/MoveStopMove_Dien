using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation.Editor;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{

    public Bot bot;
    public PlayerController player;
    public Transform point;

   
   
    private void Awake()
    {
        SpawnPlayer();
       // SpawnBot();

    }
    
    private void SpawnPlayer()
    {
        player.OnInit();
    }
    private void SpawnBot()
    {
         bot = Instantiate(bot,point.position,Quaternion.identity);
    }
  
   

   
  

}
