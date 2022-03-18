using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBaseAI : BaseAI
{
    public TPlayerType PlayerType;

    public GameObject nearestGate;
    public float wallDistanceThreshold = 2.5f;

    private GateManager _gateManager;

    new void Awake()
    {
        base.Awake();
        _gateManager = GameObject.FindGameObjectWithTag("GateManager").GetComponent<GateManager>();
        if (_gateManager == null)
            throw new NullReferenceException("GateManager is null in PlayerBaseAI");
    }

    new void Update()
    {
        base.Update();
        List<GameObject> gateGameObjects = _gateManager.GetGateList();
        foreach (GameObject gate in gateGameObjects)
        {
            Vector3 wallPosition = gate.transform.position;
            Vector3 playerPosition = transform.position;
        
            Vector3 wallPositionInXZPlane = new Vector3(wallPosition.x, 0, wallPosition.z);
            Vector3 playerPositionInXZPlane = new Vector3(playerPosition.x, 0, playerPosition.z);
        
            if(Vector3.Distance(wallPositionInXZPlane, playerPositionInXZPlane) <= wallDistanceThreshold)
            {
                nearestGate = gate;
                _gateManager.SetIsPlayerNearGate(true);
                StopNavigation();
            }
        }
    }
}
