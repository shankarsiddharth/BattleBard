using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{

	public bool persistent;
	public float duration;

	protected float _cur_duration;

	private void Update()
	{
		if (!persistent)
		{
			_cur_duration += Time.deltaTime;
			if (_cur_duration > duration)
			{
				EventManager.RaiseEffectExpiredEvent(this);
				Destroy(gameObject);
			}
		}
	}

	abstract public Stats Apply(Stats minion_stats);
}
