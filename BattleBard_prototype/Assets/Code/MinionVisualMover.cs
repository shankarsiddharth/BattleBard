using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionVisualMover : MonoBehaviour
{
    private readonly float MOVEMENT_SPEED = 0.25f;

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.parent.transform.position - transform.position)*MOVEMENT_SPEED*Time.deltaTime;
    }
}
