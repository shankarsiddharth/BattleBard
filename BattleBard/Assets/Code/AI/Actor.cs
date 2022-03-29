using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Actor : MonoBehaviour
{
    public Stats stats = new Stats();
    public float currentHealth;
    public float attackDelay;

    public StateMachine stateMachine;

    public IdleState idleState;
    public AttackingState attackingState;
    public ChasingState chasingState;

    public abstract State DefaultState { get; }

    [HideInInspector]
    public Actor target;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    [HideInInspector]
    public Vector3 moveTarget;

    [HideInInspector]
    public List<StatusEffect> currentEffects = new List<StatusEffect>();

    public float detectionRange;

    public abstract string EnemyTag { get; }

    public bool IsDead => currentHealth <= 0;

    public void Start()
    {
        currentHealth = stats.maxHealth;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine);
        attackingState = new AttackingState(this, stateMachine);
        chasingState = new ChasingState(this, stateMachine);

        Init();

        stateMachine.Initialize(DefaultState);
    }

    public void Update()
    {
        stateMachine.CurrentState.Update();
    }

    public void SetTarget(Actor t) => target = t;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    public abstract void Init();

    public abstract void Attack();
}
