using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    void Start()
    {
        EventManager.OnMinionDeathAnim		+= OnMinionDeath;
        EventManager.OnAttackAnim			+= OnMinionAttack;
		EventManager.OnEffectApplied		+= OnEffectApplied;
		EventManager.OnAreaEffectApplied	+= OnAreaEffectApplied;
    }

	private void OnAreaEffectApplied(Effect eff, Vector3 center, float radius)
	{
		// ...
	}

	private void OnEffectApplied(Effect eff, Minion target)
	{
		if (eff.VFX != null)
			eff.effectInstance = Instantiate(eff.VFX, target.transform.GetChild(0), false);
	}

	private void OnMinionAttack(Minion m)
	{
		// ...
	}

	private void OnMinionDeath(Minion m)
	{
	}

	
}
