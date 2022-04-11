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
    public DeathState deathState;

    public State DefaultState;

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

    private HealthBar healthBar;

    public void Start()
    {
        currentHealth = stats.maxHealth;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine);
        attackingState = new AttackingState(this, stateMachine);
        chasingState = new ChasingState(this, stateMachine);
        deathState = new DeathState(this, stateMachine);

        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(stats.maxHealth);

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
        healthBar.SetHealth(currentHealth);

        if (IsDead)
        {
            healthBar.gameObject.SetActive(false);
            stateMachine.ChangeState(deathState);
            tag = "DeadActor";

            Dwarf dwarf = GetComponent<Dwarf>();
            if (dwarf)
            {
                GameEvents.Instance.OnDwarfDeath(dwarf);
            }
        }
    }

    public abstract void Init();

    public abstract void Attack();
}
