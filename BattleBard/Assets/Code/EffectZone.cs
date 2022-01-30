using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectZone : MonoBehaviour
{
    public Effect applied_effect;

    // Start is called before the first frame update
    void Start()
    {
        applied_effect = Instantiate(applied_effect, transform);
        applied_effect.duration = -1;
        applied_effect.persistent = true;
    }

	private void OnTriggerEnter(Collider other)
	{
        other.gameObject.TryGetComponent(out Minion m);

        if (m == null)
            return;

        EventManager.RaiseEffectAppliedEvent(applied_effect, m);
	}

	private void OnTriggerExit(Collider other)
	{
        other.gameObject.TryGetComponent(out Minion m);

        if (m == null)
            return;

        EventManager.RaiseZoneEffectExpired(applied_effect, m);
    }
}
