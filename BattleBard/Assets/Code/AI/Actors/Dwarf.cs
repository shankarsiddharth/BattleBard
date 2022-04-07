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
        FindNextCheckpoint();
    }

    private void FindNextCheckpoint()
    {
        if (checkpoints.Count != 0)
        {
            float closestDist = float.MaxValue;
            Transform closestCheckpoint = null;

            foreach (Transform checkpoint in checkpoints)
            {
                float distance = Vector3.Distance(checkpoint.position, transform.position);
                if (distance < closestDist)
                {
                    closestDist = distance;
                    closestCheckpoint = checkpoint;
                }
            }

            if (closestCheckpoint)
            {
                Vector3 movePosition = new Vector3(closestCheckpoint.position.x, 0, closestCheckpoint.position.z);
                moveTarget = movePosition;
            }
        }
        else
        {
            stateMachine.ChangeState(idleState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            print("Checkpoint Entered");
            checkpoints.Remove(other.transform);

            FindNextCheckpoint();
        }
    }
}
