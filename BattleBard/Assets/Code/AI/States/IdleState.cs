using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        // TODO Activate idle animation
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
