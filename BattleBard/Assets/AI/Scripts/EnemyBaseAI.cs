using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAI : MonoBehaviour
{
    Animator EnemyBaseAIAnimator;
    public GameObject PlayerAIGameObject;

    public float AttackStartTime = 0.25f;
    public float AttackRate = 0.25f;
    
    public GameObject GetPlayerAIGameObject()
    {
        return PlayerAIGameObject;
    }

    void EnemyAIAttack()
    {
        //TODO: Implement Attack Code
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
    void Start () 
    {
        EnemyBaseAIAnimator = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update () 
    {
        EnemyBaseAIAnimator.SetFloat("distance", Vector3.Distance(transform.position,PlayerAIGameObject.transform.position));	
    }
}
