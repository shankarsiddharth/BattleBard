using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #region Minion Events
    //Change types from int to Minion when class is implemented
    public UnityEvent<int, int> onMinionAttackStarted;
    public UnityEvent<int> onMinionDeath;
    public UnityEvent<Dwarf> onDwarfDeath;

    public void OnMinionAttackStarted(int attackerId, int otherId)
    {
        onMinionAttackStarted.Invoke(attackerId, otherId);
    }

    public void OnMinionDeath(int minionId)
    {
        onMinionDeath.Invoke(minionId);
    }

    public void OnDwarfDeath(Dwarf dwarf)
    {
        onDwarfDeath.Invoke(dwarf);
    }
    #endregion

    #region Effect Events

    //Change types after dependant classes are implemented
    public UnityEvent onAreaEffectApplied;
    public UnityEvent onEffectApplied;
    public UnityEvent onEffectExpired;

    public void OnAreaEffectApplied()
    {
        onAreaEffectApplied.Invoke();
    }

    public void OnEffectApplied()
    {
        onEffectApplied.Invoke();
    }

    public void OnEffectExpired()
    {
        onEffectExpired.Invoke();
    }
    #endregion

    #region Drum Events
    //Maybe change drum id to enum again after drum class implementation?
    public UnityEvent<Drums> onDrumPlayed;

    //Add parameters after implementations of effects and stuff
    public UnityEvent<ComboBase, int, Vector3> onDrumComboCompleted;

    public UnityEvent<Note> onNoteEvaluated;

    public void OnDrumPlayed(Drums drum)
    {
        onDrumPlayed.Invoke(drum);
    }
    
    public void OnDrumComboCompleted(ComboBase effect, int level, Vector3 pos)
    {
        onDrumComboCompleted.Invoke(effect, level, pos);
    }

    public void OnNoteEvaluated(Note note)
    {
        onNoteEvaluated.Invoke(note);
    }
    #endregion

    #region Lane Events
    public UnityEvent<int> onUnitSpawnedInLane;

    public void UnitSpawnedInLane(int waveCount)
    {
        onUnitSpawnedInLane.Invoke(waveCount);
    }
    #endregion

    #region Event Triggers
    //Triggers on event completion
    public UnityEvent<int> onEventCompleted;
    public UnityEvent<Gate> onGateDestroyed;

    public void OnEventCompleted(int eventId)
    {
        onEventCompleted.Invoke(eventId);
    } 
    
    public void OnGateDestroyed(Gate gate)
    {
        onGateDestroyed.Invoke(gate);
    }
    #endregion

    #region Narrative Pieces
    public UnityEvent onNarrativePieceStart;
    public UnityEvent onNarrativePieceCompleted;
    
    public void OnNarrativePieceStart()
    {
        onNarrativePieceStart.Invoke();
    }
    
    public void OnNarrativePieceCompleted()
    {
        onNarrativePieceCompleted.Invoke();
    }

    #endregion


    #region Inspector
    /*  Functions for the inspector (doesn't like enums, etc.)  */
    
    public void KeyboardDrum(InputAction.CallbackContext context)
	{
        if (context.performed)
        {
            switch (context.action.name)
			{
                case "LeftShoulder":
                    GameEvents.Instance.OnDrumPlayed(Drums.LeftShoulder);
                    break;
                case "RightShoulder":
                    GameEvents.Instance.OnDrumPlayed(Drums.RightShoulder);
                    break;
                case "RightStomach":
                    GameEvents.Instance.OnDrumPlayed(Drums.RightStomach);
                    break;
                case "LeftThigh":
                    GameEvents.Instance.OnDrumPlayed(Drums.LeftThigh);
                    break;
                case "RightThigh":
                    GameEvents.Instance.OnDrumPlayed(Drums.RightThigh);
                    break;
                case "LeftStomach":
                    GameEvents.Instance.OnDrumPlayed(Drums.LeftStomach);
                    break;
                /*case "Combo":
                    AkSoundEngine.SetState("Tier1", "Attacking");
                    Debug.Log("Combo");
                    break;*/
            }
        }
    }

    #endregion
}
