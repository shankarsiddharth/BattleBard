using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAI : MonoBehaviour
{
    public Stats stats;
    public TEnemyType EnemyType;
    public float currentHealth;
    
    Animator EnemyBaseAIAnimator;
    public GameObject PlayerAIGameObject;

    public List<GameObject> GuardPaths;
    
    public float AttackStartTime = 0.25f;
    public float AttackRate = 1.0f;


    private GameObject EnemyRootGameObject;

    public GameObject GetRootGameObject()
    {
        return EnemyRootGameObject;
    }

    public GameObject GetPlayerAIGameObject()
    {
        return PlayerAIGameObject;
    }

    void EnemyAIAttack()
    {
        //TODO: Implement Attack Code
        if(PlayerAIGameObject == null)
            return;
        
        PlayerBaseAI playerBaseAI = PlayerAIGameObject.GetComponent<PlayerBaseAI>();
        Stats playerStats = playerBaseAI.stats;

        if (playerBaseAI.currentHealth > 0)
        {
            //Attack
            playerBaseAI.currentHealth -= stats.damage;
        }
        
    }

    public void StopEnemyAIAttack()
    {
        CancelInvoke("EnemyAIAttack");
    }

    public void StartEnemyAIAttack()
    {
        InvokeRepeating("EnemyAIAttack", AttackStartTime, AttackRate);
    }

    // Use this for initialization
    void Awake () 
    {
        EnemyBaseAIAnimator = GetComponent<Animator>();
        if (GuardPaths.Count > 0)
        {
            gameObject.transform.position = GuardPaths[0].transform.position;
        }
    }
	
    // Update is called once per frame
    void Update () 
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }

        if (PlayerAIGameObject == null)
        {
            EnemyBaseAIAnimator.SetBool("hasNoTarget", true);
            EnemyBaseAIAnimator.SetFloat("distance", 1000.0f);
            return;
        }
        
        EnemyBaseAIAnimator.SetFloat("distance", Vector3.Distance(transform.position,PlayerAIGameObject.transform.position));	
    }
}
