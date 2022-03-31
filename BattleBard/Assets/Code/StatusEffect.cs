using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect")]
public class StatusEffect : ScriptableObject
{
	[Tooltip("The duration of the tier.")]
	public float[] tierDuration;
	[Tooltip("The effects (applied multiplicitively) on the Stats of a minion. >1 for increase, <1 for decrease.\n Each item in the list represents a tier of the effect.")]
	public Stats[] multiplicitiveChanges;
	[Tooltip("The prefab of the effect to be applied as a child to the minions.")]
	public GameObject[] effectVFX;

	// Potentially takes GameObject instead of BaseAI and finds the PlayerBaseAI or EnemyBaseAI components
	public IEnumerator StartTimer(int tier, Actor actor)
	{
		Debug.Log("Started");

		if (!actor.currentEffects.Contains(this))
		{
			// Add VFX
			GameObject vfx = Instantiate(effectVFX[tier], actor.transform);
			actor.currentEffects.Add(this);
			actor.stats *= multiplicitiveChanges[tier];

			yield return new WaitForSeconds(tierDuration[tier]);

			if (actor != null)
			{
				actor.currentEffects.Remove(this);
				actor.stats /= multiplicitiveChanges[tier];
				Destroy(vfx);
			}

			Debug.Log("Done.");
		}
	}
}