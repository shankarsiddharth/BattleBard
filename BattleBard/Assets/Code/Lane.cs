using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [Tooltip("List of points along the lane's path. First point is green.")]
    public List<Vector3> nav_points;

    [HideInInspector]
    public List<Minion> allied_minions;
    [HideInInspector] 
    public List<Minion> enemy_minions;
    [HideInInspector]
    public List<Effect> current_effects;

    // Start is called before the first frame update
    void Start()
    {

    }


	#region Gizmos
	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        foreach (Vector3 point in nav_points)
        {
            Gizmos.DrawWireSphere(point, 0.1f);
            Gizmos.color = Color.white;
        }
	}
	private void OnDrawGizmosSelected()
	{
		for (int i=0; i<nav_points.Count-1; i++)
		{
            Gizmos.DrawLine(nav_points[i], nav_points[i+1]);
		}
	}
    #endregion
}
