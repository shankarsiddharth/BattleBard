using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "NewStatusEffect", menuName = "Status Effect")]
public class StatusEffect : ScriptableObject
{
	[Tooltip("The duration of the tier.")]
	public float[] tierDuration;
	[Tooltip("The effects (applied multiplicitively) on the Stats of a minion. >1 for increase, <1 for decrease.\n Each item in the list represents a tier of the effect.")]
	public Stats[] multiplicitiveChanges;
	[Tooltip("The prefab of the effect to be applied as a child to the minions.")]
	public GameObject[] effectVFX;

	// Potentially takes GameObject instead of BaseAI and finds the PlayerBaseAI or EnemyBaseAI components
	public IEnumerator StartTimer(int tier, BaseAI ai)
	{
		Debug.Log("Started");

		if (!ai.stats.currentEffects.Contains(this))
		{
			// Add VFX
			GameObject vfx = Instantiate(effectVFX[tier], ai.transform);
			ai.stats.currentEffects.Add(this);
			ai.stats *= multiplicitiveChanges[tier];

			yield return new WaitForSeconds(tierDuration[tier]);

			if (ai != null)
			{
				ai.stats.currentEffects.Remove(this);
				ai.stats /= multiplicitiveChanges[tier];
				Destroy(vfx);
			}

			Debug.Log("Done.");
		}
	}
}