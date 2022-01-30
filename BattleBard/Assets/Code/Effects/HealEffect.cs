using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : Effect
{
	public float percentageHealed = 0.3f;

	private float _last_duration = 0f;

	public override Stats Apply(Stats minion_stats)
	{
		minion_stats.health += (duration - _last_duration) * (minion_stats.max_health * percentageHealed / duration);

		_last_duration = duration;
		return minion_stats;
	}
}
