using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAIAttackBehaviour : EnemyBaseAIStateMachine 
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        EnemyAIGameObject.GetComponent<EnemyBaseAI>().StartEnemyAIAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAIGameObject.transform.LookAt(TargetAIGameObject.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        EnemyAIGameObject.GetComponent<EnemyBaseAI>().StopEnemyAIAttack();
    }

}

