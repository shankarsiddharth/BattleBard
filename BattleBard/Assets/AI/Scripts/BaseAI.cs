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

    public GameObject SearchTarget(List<GameObject> targetOptions)
    {
        float closestDistance = float.MaxValue;
        GameObject closestBaseAI = null;
  
        foreach(GameObject option in targetOptions)
        {
            Vector3 distanceVector = this.transform.position - option.transform.position;
            float distance = distanceVector.magnitude;

            if(distance < closestDistance)
            {
                closestBaseAI = option;
                closestDistance = distance;
            }
        }
        return closestBaseAI;
    }

    protected void Update()
    {
        if (currentHealth <= 0)
        {
            CombatManager.Instance.RemoveGameObjectFromBattleground(this);
            Destroy(gameObject);
        }

        if (target == null)
        {
            animator.SetBool("hasNoTarget", true);
            animator.SetBool("enemyInRange", false);
            animator.SetFloat("distance", 1000.0f);
            return;
        }
        else
        {
            animator.SetBool("hasNoTarget", false);
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
