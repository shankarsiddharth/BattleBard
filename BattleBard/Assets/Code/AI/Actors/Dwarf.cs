using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Dwarf : Actor
{
    public MovingState movingState;
    public override State DefaultState => movingState;

    [HideInInspector]
    public List<Transform> checkpoints;

    public override string EnemyTag => "Orc";

    public override void Init()
    {
        FindCheckpoints();

        movingState = new MovingState(this, stateMachine);
    }

    private void FindCheckpoints()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Select((go) => go.transform).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            checkpoints.Remove(other.transform);

            if (checkpoints.Count != 0)
            {
                float closestDist = float.MaxValue;
                Transform closestCheckpoint = null;

                foreach (Transform checkpoint in checkpoints)
                {
                    float distance = Vector3.Distance(closestCheckpoint.position, transform.position);
                    if (distance < closestDist)
                    {
                        closestDist = distance;
                        closestCheckpoint = checkpoint;
                    }
                }

                if (closestCheckpoint)
                    moveTarget = closestCheckpoint.position;
            }
            else
            {
                stateMachine.ChangeState(idleState);
            }
        }
    }
}
