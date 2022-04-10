using UnityEngine;

public abstract class Orc : Actor
{
    public GuardingState guardingState;


    public Transform[] guardPoints;

    public int guardPointIndex = 0;

    public override string EnemyTag => "Dwarf";

    public override void Init()
    {
        guardingState = new GuardingState(this, stateMachine);
        DefaultState = guardingState;
    }
}
