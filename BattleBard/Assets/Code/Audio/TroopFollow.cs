using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopFollow : MonoBehaviour
{
    public Battalion battalion;

    void Awake()
    {
        battalion = FindObjectOfType<Battalion>();
        if (battalion == null)
        {
            throw new NullReferenceException("Battalion is null in Troop");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = battalion.GetMidPoint();

        if (midpoint != Vector3.zero)
            transform.position = midpoint;
    }
}