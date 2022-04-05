using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerZone : MonoBehaviour
{
    public int eventId;
    public bool hasNarratedEnding;
    private void OnTriggerEnter(Collider other)
    {
        Dwarf dwarf = other.GetComponent<Dwarf>();

        if (dwarf != null) {
            GameEvents.Instance.OnEventCompleted(eventId);

            if (hasNarratedEnding)
            {
                GameEvents.Instance.OnNarrativePieceStart();
            }
        }
    }
 
}
