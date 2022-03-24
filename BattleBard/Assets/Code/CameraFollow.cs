using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Battalion battalion;

    void Start()
    {
        battalion = FindObjectOfType<Battalion>();
        if (battalion == null)
        {
            throw new NullReferenceException("Combat Manager Object is null in CameraFollow");
        }
    }

    void Update()
    {
        Vector3 midpoint = battalion.GetMidPoint();

        if (midpoint != Vector3.zero)
            transform.position = midpoint;
    }
}