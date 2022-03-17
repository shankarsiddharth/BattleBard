using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : BaseAIStateMachine
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        NavMeshAgentObject.speed = animator.GetComponent<BaseAI>().stats.movementSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(TargetAIGameObject == null)
            return;
        
        Vector3 targetPosition;
        NavMeshAgentObject.SetDestination(TargetAIGameObject.transform.position);
        //rotate towards target
        /*Vector3 direction = TargetAIGameObject.transform.position - EnemyAIGameObject.transform.position;
        EnemyAIGameObject.transform.rotation = Quaternion.Slerp(EnemyAIGameObject.transform.rotation,
            Quaternion.LookRotation(direction), 
            AIRotationSpeed * Time.deltaTime);
        EnemyAIGameObject.transform.Translate(0, 0, Time.deltaTime * AISpeed);*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        //EnemyAIGameObject.transform.LookAt(TargetAIGameObject.transform.position);
    }
    
}