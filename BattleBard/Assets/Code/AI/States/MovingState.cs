using UnityEngine;

public class MovingState : State
{
    public MovingState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        // TODO Create animator parameters and set them here.
        float animStartRandomizer = Random.Range(0, (float)1);
        Debug.Log(animStartRandomizer);
        actor.animator.Play("Guard", 0, animStartRandomizer);

        actor.navMeshAgent.SetDestination(actor.moveTarget);
        actor.navMeshAgent.isStopped = false;
    }

    public override void Update()
    {
        base.Update();

        if (!actor.target) SearchForTarget();

        if (actor.navMeshAgent.destination != actor.moveTarget)
            actor.navMeshAgent.SetDestination(actor.moveTarget);
    }

    public override void Exit()
    {
        base.Exit();

        actor.navMeshAgent.isStopped = true;
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
