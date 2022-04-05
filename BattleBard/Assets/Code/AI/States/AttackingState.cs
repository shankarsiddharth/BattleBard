using UnityEngine;

public class AttackingState : State
{
    public AttackingState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    private float actorAttackSpeed;
    
    public override void Enter()
    {
        base.Enter();

        actorAttackSpeed = actor.stats.attackSpeed;
      
        actor.InvokeRepeating(nameof(actor.Attack), actor.attackDelay, 1 / actor.stats.attackSpeed);

        actor.navMeshAgent.isStopped = true;
    }

    public override void Update()
    {
        if (actor.target == null || actor.target.IsDead)
        {
            stateMachine.ChangeState(actor.DefaultState);
            return;
        }

        actor.navMeshAgent.SetDestination(actor.target.transform.position);

        if (!actor.navMeshAgent.pathPending && actor.navMeshAgent.remainingDistance > actor.stats.range)
        {
            stateMachine.ChangeState(actor.chasingState);
            return;
        }

        if (!actor.navMeshAgent.pathPending && actor.navMeshAgent.remainingDistance > actor.detectionRange)
        {
            stateMachine.ChangeState(actor.DefaultState);
            return;
        }

        base.Update();

        if(actorAttackSpeed != actor.stats.attackSpeed)
        {
            actor.CancelInvoke();
            actorAttackSpeed = actor.stats.attackSpeed;
            actor.InvokeRepeating(nameof(actor.Attack), actor.attackDelay, 1 / actor.stats.attackSpeed);
        }

        actor.transform.LookAt(actor.target.transform);
    }

    public override void Exit()
    {
        base.Exit();

        actor.target = null;
        actor.CancelInvoke();
    }
}
