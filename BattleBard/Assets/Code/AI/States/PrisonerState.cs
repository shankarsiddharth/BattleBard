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

            if (distance <= actor.detectionRange)
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

        if (!furthestDwarf) return;

        if (Vector3.Distance(furthestDwarf.transform.position, actor.transform.position) <= actor.detectionRange)
        {
            Dwarf dwarfComponent = actor.GetComponent<Dwarf>();
            actor.DefaultState = dwarfComponent.movingState;
            actor.tag = "Dwarf";
            actor.transform.SetParent(dwarfBattalion.transform);
            dwarfComponent.checkpoints = furthestDwarf.GetComponent<Dwarf>().checkpoints;
            actor.transform.Find("HP Canvas").gameObject.SetActive(true);
            stateMachine.ChangeState(actor.DefaultState);
        }
    }
}
