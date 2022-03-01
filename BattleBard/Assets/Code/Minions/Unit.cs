using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType
{
    AOECircular,
    AOEConic,
    SingleTarget
}

public abstract class Unit : MonoBehaviour
{
    public AttackType attackType = AttackType.SingleTarget;
    public Stats stats;
    public float currentHealth;
    public Unit targetUnit;
    public bool isAlly = true;

    public abstract void Attack();

    public abstract void Die();

    public abstract void SetTargetUnit();

    public abstract void SetDestination();
    
    public abstract void ApplyStatusEffect();
}
