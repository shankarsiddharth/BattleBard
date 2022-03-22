using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAIStateMachine : StateMachineBehaviour
{
    public NavMeshAgent NavMeshAgentObject;
    public GameObject AIGameOBject;
    public Animator animationAnimator;
    public GameObject TargetAIGameObject;
    public float AISpeed = 1.0f;
    public float AIRotationSpeed = 1.0f;
    public float AIDistanceTolerence = 1.25f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AIGameOBject = animator.gameObject;
        NavMeshAgentObject = AIGameOBject.GetComponent<NavMeshAgent>();
        
        //TODO: Find A Target Player Character to Attack/Focus
        TargetAIGameObject = AIGameOBject.GetComponent<BaseAI>().GetTarget();
        animationAnimator = AIGameOBject.transform.GetChild(0).GetComponent<Animator>();
    }
}
