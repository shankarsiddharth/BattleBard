using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackBehavior : BaseAIStateMachine 
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        NavMeshAgentObject.speed = 0.0f;
        animationAnimator.Play("Attack");
        AIGameOBject.GetComponent<BaseAI>().StartAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (TargetAIGameObject == null)
        {
            return;
        }*/
            
        /*Vector3 directionFromEnemyToPlayer =
            (TargetAIGameObject.transform.position - EnemyAIGameObject.transform.position).normalized;
        float dotProduct = Vector3.Dot(directionFromEnemyToPlayer, EnemyAIGameObject.transform.forward);
        /*
         * dotProduct = 1 => Enemy Looking at Player
         * dotProduct = -1 => Enemy and Player are facing opposite direction
         * dotProduct = between 0 & 1 => Enemy is roughly looking at Player's direction
        #1#
        if (dotProduct <= 0.95f)
        {
            EnemyAIGameObject.transform.LookAt(TargetAIGameObject.transform.forward);
        }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        AIGameOBject.GetComponent<BaseAI>().StopAttack();
    }

}

