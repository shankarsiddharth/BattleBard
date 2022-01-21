using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Minion : MonoBehaviour
{
    public static readonly float BASE_ATTACK_SPEED = 5f;

    public Stats minion_stats;
    [Header("Movement")]
    [Tooltip("Should be assigned to this Minion's current lane. Will eventually be set by Lane Manager (or equivilent)")]
    public Lane cur_lane;
    [HideInInspector]
    public int pointIndex;

    [Header("Config")]
    [Tooltip("How precise minions need to be to the nav_point before they're considered to be moving to the next.")]
    public float movement_precision;

    #if UNITY_EDITOR
    public bool debug;
    #endif

    [Header("Effects")]
    public List<Effect> current_effects;

    private float _attack_cooldown;
    private Stats _default_stats;

    // Start is called before the first frame update
    void Start()
    {
        #region Events
        EventManager.OnEffectApplied += OnEffectApplied;
        EventManager.OnEffectExpired += OnEffectExpired;
		#endregion

		if (minion_stats.allied)
        {
            transform.position = cur_lane.nav_points[0];
            pointIndex = 1;
        }
        else
        {
            transform.position = cur_lane.nav_points[cur_lane.nav_points.Count-1];
            pointIndex = cur_lane.nav_points.Count - 2;
        }

        // Keep a copy around
        _default_stats = minion_stats;
    }

	private void Update()
	{
        ApplyStats();

        if (_attack_cooldown > 0)
            _attack_cooldown -= Time.deltaTime;

        bool valid = true;

        Minion m = FindTargetInRange();
        if (m && valid)
		{
            Attack(m);
            valid = false;
		}

        // Move if no enemies within attack range
        if (valid)
            Move();

        if (minion_stats.health <= 0f)
        {
            EventManager.RaiseMinionDeathAnimEvent(this);
            EventManager.RaiseMinionDeathEvent(this);
            Destroy(gameObject);
        }
    }

    private void ApplyStats()
    {
        // Health is an exception - damage taken cannot be reverted to default.
        _default_stats.health = minion_stats.health;
        // Revert all others so that effects can be applied
        minion_stats = _default_stats;

        foreach (Effect e in current_effects)
            minion_stats = e.Apply(minion_stats);

        print(minion_stats.movement_speed);
	}
	private void Move()
	{
        transform.position += (cur_lane.nav_points[pointIndex] - transform.position).normalized * minion_stats.movement_speed * Time.deltaTime;

        // If the minion is close enough, start moving towards the next point.
        if ((transform.position - cur_lane.nav_points[pointIndex]).magnitude < movement_precision)
        {
            if (minion_stats.allied)
            {
                if (pointIndex < cur_lane.nav_points.Count - 1)
                    pointIndex++;
            }
            else
                if (pointIndex > 0)
                pointIndex--;

        }
    }
    private Minion FindTargetInRange()
    {
        // Hacky hacky hackkkkyy
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Minion");
        foreach (GameObject go in gos)
        {
            Minion other = go.GetComponent<Minion>();

            // Ignore self
            if (other == this)
                continue;

            // Ignore already dead Minions
            if (!other)
                continue;

            if ((other.transform.position - transform.position).magnitude < minion_stats.attack_range)
            {
                return other;
            }
        }
        return null;
    }
    private void Attack(Minion other)
	{
        if (_attack_cooldown <= 0f)
		{
            EventManager.RaiseAttackAnimEvent(this);
            EventManager.RaiseMinionAttackEvent(this, other);

            if (minion_stats.applied_effect)
                EventManager.RaiseEffectAppliedEvent(minion_stats.applied_effect, other);

            other.minion_stats.health -= minion_stats.damage;
            _attack_cooldown += BASE_ATTACK_SPEED / minion_stats.attack_speed;
		}
	}

    #region Events
    private void OnEffectApplied(Effect eff, Minion target)
    {
        if (target == this)
            current_effects.Add(eff);
    }

    private void OnEffectExpired(Effect eff)
    {
        if (current_effects.Contains(eff))
            current_effects.Remove(eff);
    }

    #endregion


    #region Gizmos
    private void OnDrawGizmosSelected()
	{
        if (debug)
            Gizmos.DrawWireSphere(transform.position, movement_precision);
	}
    #endregion
}
