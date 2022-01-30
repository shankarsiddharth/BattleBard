using UnityEngine;
using System;

[Serializable]
public struct Stats
{
    public bool allied;

    [Header("Combat")]
    [Tooltip("Whether or not this Minion is considered ranged. Does nothing, might be removed?")]
    public bool ranged;
    [Tooltip("Whether or not this Minion applies an AOE effect.")]
    public bool AOE;
    public float AOE_range;
    [Tooltip("The rate of attack as a divisor of BASE_ATTACK_SPEED in seconds. Higher equals faster.")]
    public float attack_speed;
    [Tooltip("The range (in Unity units) that this Minion can attack.")]
    public float attack_range;
    [Tooltip("The damage that this Minion inflicts on its enemies per attack.")]
    public float damage;
    public float max_health;
    [Tooltip("The current health of the Minion.")]
    public float health;
    [Tooltip("The Effect that is applied when this Minion attacks another.")]
    public Effect applied_effect;

    [Header("Movement")]
    public float movement_speed;
}