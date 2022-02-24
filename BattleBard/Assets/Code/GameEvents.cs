using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public void OnMinionAttackStarted(int attackerId, int otherId)
    {
        onMinionAttackStarted.Invoke(attackerId, otherId);
    }

    public void OnMinionDeath(int minionId)
    {
        onMinionDeath.Invoke(minionId);
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
    public UnityEvent onDrumComboCompleted;

    public void OnDrumPlayed(Drums drum)
    {
        onDrumPlayed.Invoke(drum);
    }
    
    public void OnDrumComboCompleted()
    {
        onDrumComboCompleted.Invoke();
    }
    #endregion

    #region Lane Events
    public UnityEvent<int> onUnitSpawnedInLane;

    public void UnitSpawnedInLane(int waveCount)
    {
        onUnitSpawnedInLane.Invoke(waveCount);
    }
    #endregion

    #region Path Events
    //Change Event parameter to Path object after implementation
    public UnityEvent<int> onPathCompleted;
    public UnityEvent<int> onPathStart;

    public void OnPathCompleted(int pathId)
    {
        onPathCompleted.Invoke(pathId);
    }

    public void OnPathStart(int pathId)
    {
        onPathStart.Invoke(pathId);
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
    
    public void KeyboardDrum(int drumID)
	{
        OnDrumPlayed((Drums)drumID);
	}

    #endregion
}
