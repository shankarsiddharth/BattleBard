using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [HideInInspector]
    public List<Gate> gates;
    private Battalion _battalion;
    private bool _isPlayerNearAGate = false;
    
    void Awake()
    {
        gates = FindObjectsOfType<Gate>().ToList();
        _battalion = FindObjectOfType<Battalion>();
    }

    void Start()
    {
        GameEvents.Instance.onDrumComboCompleted.AddListener(OnDrumComboCompleted);
        GameEvents.Instance.onGateDestroyed.AddListener(RemoveGate);
    }

    private void OnDrumComboCompleted(ComboBase combo, int comboLevel, Vector3 position)
    {
        if (_battalion.nearestGate)
        {
            _battalion.nearestGate.TakeDamage(1);
        }
    }

    void Update()
    {
        if (_battalion.isNearGate)
        {

        }
    }

    public void RemoveGate(Gate gate)
    {
        gates.Remove(gate);
    }

    public void SetIsPlayerNearGate(bool isPlayerNearGate)
    {
        _isPlayerNearAGate = isPlayerNearGate;
    }

    public bool GetIsPlayerNearGate()
    {
        return _isPlayerNearAGate;
    }
}
