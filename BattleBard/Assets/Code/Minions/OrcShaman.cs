using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcShaman : Unit
{
    #region Additional Stats
    public float attackDiameter;
    #endregion

    public override void ApplyStatusEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void SetDestination()
    {
        throw new System.NotImplementedException();
    }

    public override void SetTargetUnit()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlly = false;
        attackType = AttackType.AOECircular;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
