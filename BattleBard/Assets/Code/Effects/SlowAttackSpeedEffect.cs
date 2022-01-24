using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAttackSpeedEffect : Effect
{
	public float AttackSpeedSlowAmount = 2.0f;
	public override Stats Apply(Stats minion_stats)
	{
		minion_stats.attack_speed = Mathf.Clamp(minion_stats.attack_speed - AttackSpeedSlowAmount, 0.1f, 100f);
		return minion_stats;
	}
}
