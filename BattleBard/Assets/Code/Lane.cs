using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [Tooltip("List of points along the lane's path. First point is green.")]
    public List<Vector3> nav_points;

    [HideInInspector]
    public List<Minion> allied_minions = new List<Minion>();
    [HideInInspector] 
    public List<Minion> enemy_minions = new List<Minion>();

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnMinionDeath += OnMinionDeath;
        EventManager.OnLaneComboComplete += OnLaneComboComplete;
    }

    // Get the minion closest to the end of this lane.
	public Minion GetFurthestMinion()
	{
        if (allied_minions.Count == 0)
            return null;

        Minion furthest = allied_minions[0];
        float distance = Vector3.Distance(furthest.transform.position, nav_points[nav_points.Count-1]);

        foreach (Minion m in allied_minions)
		{
            float newDist = Vector3.Distance(m.transform.position, nav_points[nav_points.Count - 1]);

            if (newDist < distance)
            {
                furthest = m;
                distance = newDist;
            }
		}

        return furthest;
	}

    public Vector3 GetLaneCheckpoint(int index)
	{
        return transform.position + nav_points[index];
	}
    public int GetLaneCheckpointCount()
	{
        return nav_points.Count;
	}

    private void OnMinionDeath(Minion m)
    {
        if (allied_minions.Contains(m))
            allied_minions.Remove(m);

        if (enemy_minions.Contains(m))
            enemy_minions.Remove(m);
    }
    private void OnLaneComboComplete(Effect eff, Lane lane, bool affectsAllies, bool affectsEnemies)
    {
        if (lane != this)
            return;

        print(eff);

        if (affectsAllies)
            foreach (Minion m in allied_minions)
                EventManager.RaiseEffectAppliedEvent(Instantiate(eff), m);

        if (affectsEnemies)
            foreach (Minion m in enemy_minions)
                EventManager.RaiseEffectAppliedEvent(Instantiate(eff), m);

    }

    #region Gizmos
    private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        foreach (Vector3 point in nav_points)
        {
            Gizmos.DrawWireSphere(transform.position + point, 0.1f);
            Gizmos.color = Color.white;
        }
	}
	private void OnDrawGizmosSelected()
	{
		for (int i=0; i<nav_points.Count-1; i++)
		{
            Gizmos.DrawLine(transform.position + nav_points[i], transform.position + nav_points[i+1]);
		}
	}
    #endregion
}
