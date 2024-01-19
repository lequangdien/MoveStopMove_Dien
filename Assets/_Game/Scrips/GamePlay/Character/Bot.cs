using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{


    private IState currentState;
    public Vector3 newPos;
    public float wanderRadius = 6f;
    public NavMeshAgent agent;
    public GameObject indicate;
    public TextMeshProUGUI botText;
    public Bot bot;


    public bool isTarget => Vector3.Distance(transform.position, newPos) < 0.1f;
    // [SerializeField] public Animator _animatorBot;
    public void Start()
    {
        ChangeState(new PlatrolState());

        OnInit();
        SpawnWeapon();


    }

    protected override void Update()
    {

        base.Update();
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
        if (isDead)
        {
            LevelManager.Instance.BotDeath(this);
            return;
        }

    }
    public void OnInit()
    {
        if (weaponData == null)
        {
            currentWeaponType = Weapontype.Hammer;
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
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
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);

        }
    }
}
