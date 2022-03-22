using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CombatManager combatManager;
    
    void Awake()
    {
        GameObject combatManagerObject = GameObject.FindGameObjectWithTag("CombatManager");
        if (combatManagerObject == null)
        {
            throw new NullReferenceException("Combat Manager Object is null in CameraFollow");
        }

        combatManager = combatManagerObject.GetComponent<CombatManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.GetMidPoint() != Vector3.zero)
        {
            transform.position = combatManager.GetMidPoint();
        }
    }
}