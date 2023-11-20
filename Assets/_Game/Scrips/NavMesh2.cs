using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh2 : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    [SerializeField] public Animator _animatorBot;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }

    

}
