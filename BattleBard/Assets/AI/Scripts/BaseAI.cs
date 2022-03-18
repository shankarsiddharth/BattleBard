using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAI : MonoBehaviour
{
	public Stats stats;
    public float currentHealth;

    public float AttackStartTime = 0.25f;

    public GameObject target;

    public List<GameObject> GuardPaths;

    protected Animator animator;
    protected NavMeshAgent agent;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (GuardPaths.Count > 0)
        {
            gameObject.transform.position = GuardPaths[0].transform.position;
        }
    }

    void Attack()
    {
        //TODO: Implement Attack Code
        if (target == null)
            return;

        BaseAI targetAI = target.GetComponent<BaseAI>();

        if (targetAI.currentHealth > 0)
        {
            targetAI.currentHealth -= stats.damage;
        }

    }

    public void StopAttack()
    {
        CancelInvoke(nameof(Attack));
    }

    public void StartAttack()
    {
        InvokeRepeating(nameof(Attack), AttackStartTime, 1 / stats.attackSpeed);
    }

    protected void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (target == null)
        {
            animator.SetBool("hasNoTarget", true);
            animator.SetBool("enemyInRange", false);
            animator.SetFloat("distance", 1000.0f);
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        animator.SetFloat("distance", distanceToTarget);
        animator.SetBool("enemyInRange", distanceToTarget < stats.range);
    }

    public GameObject GetTarget()
    {
        return target;
    }
    
    public void StopNavigation()
    {
        agent.isStopped = true;
    }

    public void StartNavigation()
    {
        agent.isStopped = false;
    }
}
