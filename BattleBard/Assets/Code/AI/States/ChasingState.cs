public class ChasingState : State
{
    public ChasingState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        if (!actor.target)
        {
            actor.stateMachine.ChangeState(actor.DefaultState);
            return;
        }

        // TODO Create animator parameters and set them here.
        actor.animator.SetTrigger("chase");
        actor.navMeshAgent.SetDestination(actor.target.transform.position);
        actor.navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if (actor.navMeshAgent.destination != actor.target.transform.position)
            actor.navMeshAgent.SetDestination(actor.target.transform.position);

        if (!actor.navMeshAgent.pathPending && actor.navMeshAgent.remainingDistance <= actor.stats.range)
        {
            stateMachine.ChangeState(actor.attackingState);
            return;
        }

        if (!actor.navMeshAgent.pathPending && actor.navMeshAgent.remainingDistance > actor.detectionRange)
        {
            stateMachine.ChangeState(actor.DefaultState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        actor.navMeshAgent.isStopped = false;
    }
}
