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

        actor.SetTarget(closestEnemy.GetComponent<Actor>());
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        SearchForBattalion();
    }

    private void SearchForBattalion()
    {
        if (!actor.target.IsDead) return;

        Battalion dwarfBattalion = GameObject.FindObjectOfType<Battalion>();
        GameObject[] dwarves = GameObject.FindGameObjectsWithTag("Dwarf");

        float closestDist = float.MaxValue;
        GameObject closestDwarf = null;

        foreach (GameObject dwarf in dwarves)
        {
            float distance = Vector3.Distance(dwarf.transform.position, actor.transform.position);
            if (distance < closestDist)
            {
                closestDist = distance;
                closestDwarf = dwarf;
            }
        }

        if (!closestDwarf) return;

        if (Vector3.Distance(closestDwarf.transform.position, actor.transform.position) <= actor.detectionRange)
        {
            Dwarf dwarfComponent = actor.GetComponent<Dwarf>();
            actor.DefaultState = dwarfComponent.movingState;
            actor.tag = "Dwarf";
            actor.transform.SetParent(dwarfBattalion.transform);
            dwarfComponent.checkpoints = closestDwarf.GetComponent<Dwarf>().checkpoints;
            stateMachine.ChangeState(actor.DefaultState);
        }
    }
}
