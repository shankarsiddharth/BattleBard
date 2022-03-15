using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerBaseAI : MonoBehaviour
{
    public Stats stats;
    public TPlayerType PlayerType;
    public float currentHealth; 

    private GameObject playerRootGameObject;

    public GameObject GetRootGameObject()
    {
        return playerRootGameObject;
    }
}
