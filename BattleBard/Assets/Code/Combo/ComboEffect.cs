using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ComboEffect : MonoBehaviour
{
    [Tooltip("If this combo has a custom effect tick this.")]
    public bool hasCustomEffect;

    [Header("Status Effect")]
    [Tooltip("Assign this to apply a status effect to all allies/enemies in lane")]
    public StatusEffect effectApplied;
    public bool appliesToEnemies;
    public bool appliesToAllies;
}
