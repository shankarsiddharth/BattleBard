using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseAIStateMachine : StateMachineBehaviour
{
    public NavMeshAgent NavMeshAgentObject;
    public GameObject EnemyAIGameObject;
    public GameObject TargetAIGameObject;
    public float AISpeed = 1.0f;
    public float AIRotationSpeed = 1.0f;
    public float AIDistanceTolerence = 1.25f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAIGameObject = animator.gameObject;
        NavMeshAgentObject = EnemyAIGameObject.GetComponent<NavMeshAgent>();
        
        //TODO: Find A Target Player Character to Attack/Focus
        TargetAIGameObject = EnemyAIGameObject.GetComponent<EnemyBaseAI>().GetPlayerAIGameObject();
    }
}
