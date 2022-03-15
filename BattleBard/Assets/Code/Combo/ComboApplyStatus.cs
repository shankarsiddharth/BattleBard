using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboApplyStatus : ComboBase
{
    public StatusEffect statusEffect;
	public GameObject PlayerUnits;

    override public void ComboPlayed(ComboBase effect, int level, Vector3 pos)
	{
		PlayerUnits = _combatManager.PlayerUnits;
        if (effect.affectsAllies)
		{
			foreach (Transform childTransform in _combatManager.PlayerUnits.transform)
			{
				childTransform.gameObject.TryGetComponent(out BaseAI ai);
				StartCoroutine(statusEffect.StartTimer(level, ai));
			}
		}
	}
}
