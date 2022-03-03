using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAIGuardBehaviour : EnemyBaseAIStateMachine 
{

    public List<GameObject> GuardPaths;
    int CurrentGuardPathIndex;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        EnemyBaseAI enemyBaseAI = EnemyAIGameObject.GetComponent<EnemyBaseAI>();
        if (enemyBaseAI.GuardPaths.Count <= 0)
        { 
            GuardPaths = GameObject.FindGameObjectsWithTag("Level1GuardPoint").ToList();
        }
        else
        {
            GuardPaths = enemyBaseAI.GuardPaths;
        }
        CurrentGuardPathIndex = 0;
        NavMeshAgentObject.speed = 1.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {

        if(GuardPaths.Count == 0) 
            return;

        Vector3 GuardPathPosition = GuardPaths[CurrentGuardPathIndex].transform.position;
        Vector3 EnemyAIGameObjectPosition = EnemyAIGameObject.transform.position;
        Quaternion EnemyAIGameObjectRotation = EnemyAIGameObject.transform.rotation;
        
        Vector3 GuardPathPositionInXZPlane = new Vector3(GuardPathPosition.x, 0, GuardPathPosition.z);
        Vector3 EnemyAIGameObjectPositionInXZPlane = new Vector3(EnemyAIGameObjectPosition.x, 0, EnemyAIGameObjectPosition.z);
        
        if(Vector3.Distance(GuardPathPositionInXZPlane, EnemyAIGameObjectPositionInXZPlane) < AIDistanceTolerence)
        {
            CurrentGuardPathIndex++;
            if(CurrentGuardPathIndex >= GuardPaths.Count)
            {
                CurrentGuardPathIndex = 0;
            }	
        }
        
        //Update the Position based on the current path index
        GuardPathPosition = GuardPaths[CurrentGuardPathIndex].transform.position;

        NavMeshAgentObject.SetDestination(GuardPathPosition);
        
        /*//Rotate towards target
        Vector3 direction = GaurdPathPosition - EnemyAIGameObjectPosition;
        EnemyAIGameObject.transform.rotation = Quaternion.Slerp(EnemyAIGameObjectRotation, Quaternion.LookRotation(direction), AIRotationSpeed * Time.deltaTime);
        EnemyAIGameObject.transform.Translate(0, 0, Time.deltaTime * AISpeed);*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
	
    }

}

