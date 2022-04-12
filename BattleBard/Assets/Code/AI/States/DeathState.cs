using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathState : State
{
    public DeathState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        actor.target = null;
        actor.navMeshAgent.enabled = false;
        actor.animator.SetTrigger("death");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}