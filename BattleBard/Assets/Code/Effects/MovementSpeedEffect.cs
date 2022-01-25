using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedEffect : Effect
{
	[Tooltip("The raw amount to change by. Negative to slow, positive to speed up. Applied before the scalar.")]
	public float rawChange = 2f;
	[Tooltip("The mulitplicitive change of speed. Above 1 to speed up, below 1 to slow down.")]
	public float scalarChange = 1f;

	public override Stats Apply(Stats minion_stats)
	{
		minion_stats.movement_speed = Mathf.Clamp(minion_stats.movement_speed + rawChange, 0.1f, 100f);
		minion_stats.movement_speed = Mathf.Clamp(minion_stats.movement_speed * scalarChange, 0.1f, 100f);
		return minion_stats;
	}
}
