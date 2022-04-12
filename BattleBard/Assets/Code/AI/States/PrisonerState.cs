using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerState : State
{
    public PrisonerState(Actor actor, StateMachine stateMachine) : base(actor, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        actor.transform.Find("HP Canvas").gameObject.SetActive(false);
        actor.GetComponent<Dwarf>().checkpoints.Clear();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (IsAreaClear())
        {
            JoinBattalion();
        }
    }

    private bool IsAreaClear()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(actor.EnemyTag);
        int numEnemiesInArea = 0;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, actor.transform.position);

            if (distance <= actor.detectionRange && !enemy.CompareTag("DeadActor"))
            {
                numEnemiesInArea++;
            }
        }

        if (numEnemiesInArea == 0)
            return true;
        else
            return false;
    }

    private void JoinBattalion()
    {
        Battalion dwarfBattalion = GameObject.FindObjectOfType<Battalion>();
        GameObject[] dwarves = GameObject.FindGameObjectsWithTag("Dwarf");

        float leastCheckpoints = float.MaxValue;
        GameObject furthestDwarf = null;

        foreach (GameObject go in dwarves)
        {
            Dwarf dwarf = go.GetComponent<Dwarf>();

            if (dwarf.checkpoints.Count <= leastCheckpoints)
            {
                leastCheckpoints = dwarf.checkpoints.Count;
                furthestDwarf = go;
            }
        }

        Dwarf dwarfComponent = actor.GetComponent<Dwarf>();
        actor.DefaultState = dwarfComponent.movingState;
        actor.tag = "Dwarf";
        actor.transform.SetParent(dwarfBattalion.transform);
        dwarfComponent.checkpoints = furthestDwarf.GetComponent<Dwarf>().checkpoints;
        actor.transform.Find("HP Canvas").gameObject.SetActive(true);
        float closestDist = float.MaxValue;
        Transform closestCheckpoint = null;

        foreach (Transform checkpoint in dwarfComponent.checkpoints)
        {
            float distance = Vector3.Distance(checkpoint.position, actor.transform.position);
            if (distance < closestDist)
            {
                closestDist = distance;
                closestCheckpoint = checkpoint;
            }
        }

        if (closestCheckpoint)
        {
            Vector3 movePosition = new Vector3(closestCheckpoint.position.x, 0, closestCheckpoint.position.z);
            actor.moveTarget = movePosition;
        }

        stateMachine.ChangeState(actor.DefaultState);

    }
}
