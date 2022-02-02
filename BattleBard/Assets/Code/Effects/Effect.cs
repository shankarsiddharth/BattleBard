using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
	public bool persistent;
	public float duration;

	[Tooltip("The prefab of the visual effect.")]
	public GameObject VFX;

	public static int ID { get; private set; }

	protected float _cur_duration;
	protected static int last_id = 0;

	private static void Start()
	{
		ID = last_id++;
	}

	public int GetID()
	{
		return ID;
	}

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
