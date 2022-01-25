using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : Effect
{
	public float damage;
	private bool valid = true;
	public override Stats Apply(Stats minion_stats)
	{
		if (valid)
		{
			valid = false;
			minion_stats.health -= damage;
		}

		return minion_stats;
	}
}
