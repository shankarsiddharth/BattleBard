using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboApplyStatus : ComboBase
{
    public StatusEffect statusEffect;

    override public void ComboPlayed(ComboBase effect, int level, Vector3 pos)
	{
        if (effect.affectsAllies)
		{
			foreach (Transform childTransform in _combatManager.playerUnits.transform)
			{
				childTransform.gameObject.TryGetComponent(out BaseAI ai);
				StartCoroutine(statusEffect.StartTimer(level, ai));
			}
		}
	}
}
