using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerZone : MonoBehaviour
{
    public int eventId;
    public bool hasNarratedEnding;
    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit != null && unit.isAlly) {
            GameEvents.Instance.OnEventCompleted(eventId);

            if (hasNarratedEnding)
            {
                GameEvents.Instance.OnNarrativePieceStart();
            }
        }
    }
 
}
