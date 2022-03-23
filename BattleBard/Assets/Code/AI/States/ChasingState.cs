public class ChasingState : State
{
    public ChasingState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        // TODO Create animator parameters and set them here.
        actor.animator.Play("Chase");

        actor.navMeshAgent.SetDestination(actor.target.transform.position);
        actor.navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if (actor.navMeshAgent.destination != actor.target.transform.position)
            actor.navMeshAgent.SetDestination(actor.target.transform.position);

        if (actor.navMeshAgent.remainingDistance <= actor.stats.range)
            stateMachine.ChangeState(actor.attackingState);

        if (actor.navMeshAgent.remainingDistance > actor.detectionRange)
            stateMachine.ChangeState(actor.DefaultState);
    }

    public override void Exit()
    {
        base.Exit();

        actor.navMeshAgent.isStopped = false;
    }
}
