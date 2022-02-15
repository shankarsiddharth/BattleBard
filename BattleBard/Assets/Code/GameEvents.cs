using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public event Action<int, int> onMinionAttackStarted;
    public event Action<int> onMinionDeath;

    public void OnMinionAttackStarted(int attackerId, int otherId)
    {
        onMinionAttackStarted(attackerId, otherId);
    }

    public void OnMinionDeath(int minionId)
    {
        onMinionDeath(minionId);
    }
    #endregion

    #region Effect Events

    //Change types after dependant classes are implemented
    public event Action onAreaEffectApplied;
    public event Action onEffectApplied;
    public event Action onEffectExpired;

    public void OnAreaEffectApplied()
    {
        onAreaEffectApplied();
    }

    public void OnEffectApplied()
    {
        onEffectApplied();
    }

    public void OnEffectExpired()
    {
        onEffectExpired();
    }
    #endregion

    #region Drum Events
    //Maybe change drum id to enum again after drum class implementation?
    public event Action<int> onDrumPlayed;

    //Add parameters after implementations of effects and stuff
    public event Action onDrumComboCompleted;

    public void OnDrumPlayed(int drumId)
    {
        onDrumPlayed(drumId);
    }
    
    public void OnDrumComboCompleted()
    {
        onDrumComboCompleted();
    }
    #endregion

    #region Lane Events
    public event Action<int> onUnitSpawnedInLane;

    public void UnitSpawnedInLane(int waveCount)
    {
        onUnitSpawnedInLane(waveCount);
    }
    #endregion

    #region Path Events
    //Change action parameter to Path object after implementation
    public event Action<int> onPathCompleted;
    public event Action<int> onPathStart;

    public void OnPathCompleted(int pathId)
    {
        onPathCompleted(pathId);
    }

    public void OnPathStart(int pathId)
    {
        onPathStart(pathId);
    }
    #endregion
}
