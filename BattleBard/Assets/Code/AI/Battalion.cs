using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battalion : MonoBehaviour
{
   public List<Dwarf> dwarves = new List<Dwarf>();

    public Gate nearestGate;
    public bool isNearGate => nearestGate != null;

    public float gateDistanceThreshold = 2.5f;

    private GateManager _gateManager;

    void Start()
    {
        _gateManager = FindObjectOfType<GateManager>();
        dwarves = GetComponentsInChildren<Dwarf>().ToList();
        GameEvents.Instance.onGateDestroyed.AddListener(OnGateDestroyed);
    }

    private void OnGateDestroyed(Gate gate)
    {
        nearestGate = null;
        foreach (Dwarf dwarf in dwarves)
        {
            dwarf.stateMachine.ChangeState(dwarf.movingState);
        }
    }

    private void SearchNearestGate()
    {
        if (nearestGate)
            return;

        int nearDwarfCount = 0;

        foreach (Dwarf dwarf in dwarves)
        {
            foreach (Gate gate in _gateManager.gates)
            {
                Vector3 dwarfPosition = dwarf.transform.position;
                float distance = Vector3.Distance(gate.coll.ClosestPoint(dwarfPosition), dwarfPosition);

                if (distance <= gateDistanceThreshold)
                {
                    nearDwarfCount++;

                    if (nearDwarfCount >= Mathf.Ceil((float)dwarves.Count / 2))
                    {
                        nearestGate = gate;
                    }
                }

            }
        }

        if (nearestGate && _gateManager.gates.Contains(nearestGate))
        {
            foreach(Dwarf dwarf in dwarves)
            {
                dwarf.stateMachine.ChangeState(dwarf.idleState);
            }
        }
    }

    public Vector3 GetMidPoint()
    {
        if (dwarves.Count == 0)
            return Vector3.zero;

        dwarves.RemoveAll((d) => d == null);

        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;
        int count = 0;
        foreach (Dwarf dwarf in dwarves)
        {
            totalX += dwarf.transform.position.x;
            totalY += dwarf.transform.position.y;
            totalZ += dwarf.transform.position.z;
            count++;
        }
        float centerX = totalX / count;
        float centerY = totalY / count;
        float centerZ = totalZ / count;

        Vector3 midPoint = new Vector3(centerX, centerY, centerZ);

        return midPoint;
    }

    void Update()
    {
        SearchNearestGate();
    }
}
