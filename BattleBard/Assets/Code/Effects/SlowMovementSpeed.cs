using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovementSpeed : Effect
{
	public float MovementSpeedSlowAmount = 2f;

	public override Stats Apply(Stats minion_stats)
	{
		minion_stats.movement_speed = Mathf.Clamp(minion_stats.movement_speed - MovementSpeedSlowAmount, 0.1f, 100f);
		return minion_stats;
	}
}
