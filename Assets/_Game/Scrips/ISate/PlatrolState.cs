using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatrolState : IState
{
    private float timer = 0f;
    private float wannderTimer = 5f;
   public void OnEnter(Bot bot)
    {
        bot.SetDirection();
    }
    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer >= wannderTimer && !bot.isAttack) {
            bot.SetDirection();
            timer = 0f;
            wannderTimer = Random.Range(3f, 5f);
        }
        if (!bot.isTarget)
        {
            bot.isIdle = false;
            bot._animator.SetBool(ConstString.is_Idle,false);
        }
        else
        {
            bot.isIdle = true;
            bot._animator.SetBool(ConstString.is_Idle, true);
        }
    }
    public void OnExit(Bot bot) 
    { 

    }
    
}
