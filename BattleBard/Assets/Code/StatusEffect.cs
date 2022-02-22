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

	private void Awake()
	{
		// Subscribe to event manager
	}

	public IEnumerator StartTimer(int tier/*, Minion m*/)
	{
		yield return new WaitForSeconds(tierDuration[tier]);
		// Call effect expired event
		// EventManager.RaiseStatusEffectExpiredEvent(this, m);
		Debug.Log("Done.");
	}
}