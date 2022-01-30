using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
	[Tooltip("The damage that the effected Minion receives each damage tick.")]
	public float damage;
	[Tooltip("The time between damage ticks.")]
	public float tickTimer;

	private int _ticks;

	public override Stats Apply(Stats minion_stats)
	{
		if (_cur_duration > tickTimer*_ticks)
		{
			minion_stats.health -= damage;
			_ticks++;
		}

		return minion_stats;
	}
}
