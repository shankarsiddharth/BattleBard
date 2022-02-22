using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

    public Unit targetUnit;
    public bool isAlly = true;
    #region stats
    float attackRange;
    float attackSpeed;
    float attackDamage;
    float health;
    //Armor will be used later maybe
    float armor;
    float movementSpeed;
    #endregion

    public abstract void Attack();

    public abstract void Die();

    public abstract void SetTargetUnit();

    public abstract void SetDestination();
    
    public abstract void ApplyStatusEffect();
}
