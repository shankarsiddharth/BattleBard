using UnityEngine;

public class GuardingState : State
{
    public GuardingState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        // TODO Create animator parameters and set them here.
        actor.animator.SetTrigger("guard");

        GotoNextPoint();
        actor.navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if (!actor.target || actor.target.IsDead) SearchForTarget();

        if (!actor.navMeshAgent.pathPending && actor.navMeshAgent.remainingDistance < 0.2f)
            GotoNextPoint();
    }

    public override void Exit()
    {
        base.Exit();

        actor.navMeshAgent.isStopped = true;
    }

    void GotoNextPoint()
    {
        if (!(actor is Orc)) return;

        Orc orc = actor as Orc;

        // Returns if no points have been set up
        if (orc.guardPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        actor.navMeshAgent.destination = orc.guardPoints[orc.guardPointIndex].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        orc.guardPointIndex = (orc.guardPointIndex + 1) % orc.guardPoints.Length;
    }

    private void SearchForTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(actor.EnemyTag);

        float closestDist = float.MaxValue;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, actor.transform.position);
            if (distance < closestDist)
            {
                closestDist = distance;
                closestEnemy = enemy;
            }
        }

        if (!closestEnemy) return;

        if (Vector3.Distance(closestEnemy.transform.position, actor.transform.position) <= actor.detectionRange)
        {
            actor.SetTarget(closestEnemy.GetComponent<Actor>());
            stateMachine.ChangeState(actor.chasingState);
        }
    }
}
