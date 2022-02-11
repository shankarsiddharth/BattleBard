using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDamageEffect : Effect
{
	[Tooltip("The raw amount to change by. Negative to lower, positive to increase. Applied before the scalar.")]
	public float rawChange = 2f;
	[Tooltip("The mulitplicitive change of damage. Above 1 to increase, below 1 to decrease.")]
	public float scalarChange = 1f;
	public override Stats Apply(Stats minion_stats)
	{
		minion_stats.damage = Mathf.Clamp(minion_stats.damage + rawChange, 0.1f, 100f);
		minion_stats.damage = Mathf.Clamp(minion_stats.damage * scalarChange, 0.1f, 100f);
		return minion_stats;
	}
}
