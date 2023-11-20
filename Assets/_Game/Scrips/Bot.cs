using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot :Character
{
    private IState currentState;
    public Vector3 newPos;
    public float wanderRadius=6f;
    public NavMeshAgent agent; 
    public bool isTarget => Vector3.Distance(transform.position,newPos)<0.1f;
    // [SerializeField] public Animator _animatorBot;
    protected override void Start()
    {
        ChangeState(new PlatrolState());
        base.Start();

    }
    protected override void Update()
    {
       
        base.Update();
        if (currentState !=null && !isDead)
        {
            currentState.OnExecute(this);
        }

    }

    public void SetDirection()
    {
        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    public void ChangeState(IState newState)
    {
        if (currentState !=null)
        {
            currentState.OnEnter(this);
        }
        currentState= newState;
        if (currentState !=null )
        {
            currentState.OnEnter(this);
          
        }
    }
}
