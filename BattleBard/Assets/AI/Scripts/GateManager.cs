using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    private List<GameObject> _gateList;
    private bool _isPlayerNearAGate = false;
    
    void Awake()
    {
        _gateList = GameObject.FindGameObjectsWithTag("Gate").ToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        RemoveGate(_gateList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> GetGateList()
    {
        return _gateList;
    }

    public void RemoveGate(GameObject gateGameObject)
    {
        _gateList.Remove(gateGameObject);
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
