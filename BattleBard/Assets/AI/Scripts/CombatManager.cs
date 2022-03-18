using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    [Header("Values & References")] 
    public string PlayerUnitTagString = "PlayerUnits";
    public string EnemyUnitTagString = "EnemyUnits";
    public int damageToBreakWall = 3;

    [Header("Autofill Values & References")]
    public GameObject playerUnits;
    public GameObject enemyUnits;

    private List<GameObject> _playerGameObjectList = new List<GameObject>();
    private List<GameObject> _enemyGameObjectList = new List<GameObject>();

    private List<PlayerBaseAI> _playerAIList = new List<PlayerBaseAI>();
    private List<EnemyBaseAI> _enemyAIList = new List<EnemyBaseAI>();

    private GateManager _gateManager; 
    private int _damageCounter;
    
    public List<GameObject> GetPlayerGameObjectList()
    {
        InitializePlayerUnits();
        return _playerGameObjectList;
    }

    public List<GameObject> GetEnemyGameObjectList()
    {
        InitializeEnemyUnits();
        return _enemyGameObjectList;
    }

    public List<PlayerBaseAI> GetPlayerBaseAIList()
    {
        InitializePlayerUnits();
        return _playerAIList;
    }

    public List<EnemyBaseAI> GetEnemyBaseAIList()
    {
        InitializeEnemyUnits();
        return _enemyAIList;
    }

    private void Awake()
    {
        playerUnits = GameObject.FindGameObjectWithTag(PlayerUnitTagString);
        enemyUnits = GameObject.FindGameObjectWithTag(EnemyUnitTagString);

        if (playerUnits == null || enemyUnits == null)
        {
            throw new NullReferenceException("Reference missing for PlayerUnits or EnemyUnits in CombatManager");
        }

        _gateManager = GameObject.FindGameObjectWithTag("GateManager").GetComponent<GateManager>();
        if (_gateManager == null)
        {
            throw new NullReferenceException("GateManger is null in CombatManager");
        }

        foreach (Transform childTransform in playerUnits.transform)
        {
            GameObject playerGameObject = childTransform.gameObject;
            PlayerBaseAI playerBaseAI = playerGameObject.GetComponent<PlayerBaseAI>();
            /*Stats stats = playerBaseAI.stats;
            stats.maxHealth = Random.Range(11, 15);
            stats.armor = Random.Range(1, 5);
            stats.damage = Random.Range(5, 6);
            playerBaseAI.stats = stats;*/
            playerBaseAI.currentHealth = playerBaseAI.stats.maxHealth;
        }

        foreach (Transform childTransform in enemyUnits.transform)
        {
            GameObject enemyGameObject = childTransform.gameObject;
            EnemyBaseAI enemyBaseAI = enemyGameObject.GetComponent<EnemyBaseAI>();
            /*Stats stats = enemyBaseAI.stats;
            stats.maxHealth = Random.Range(1, 5);
            stats.armor = Random.Range(1, 5);
            stats.damage = Random.Range(1, 5);
            enemyBaseAI.stats = stats;*/
            enemyBaseAI.currentHealth = enemyBaseAI.stats.maxHealth;
        }
        
        //Listen to Combos
        GameEvents.Instance.onDrumComboCompleted.AddListener(ComboListener);
    }

    private void ComboListener(ComboBase combo, int level, Vector3 position)
    {
        if(!_gateManager.GetIsPlayerNearGate())
            return;
        
        _damageCounter++;
        if (_damageCounter >= damageToBreakWall)
        {
            List<PlayerBaseAI> playerBaseAis = GetPlayerBaseAIList();
            foreach (PlayerBaseAI playerBaseAi in playerBaseAis)
            {
                if (playerBaseAi.nearestGate != null)
                {
                    //Destroy the gate & break
                    _damageCounter = 0;
                    _gateManager.RemoveGate(playerBaseAi.nearestGate);
                    Destroy(playerBaseAi.nearestGate);
                    _gateManager.SetIsPlayerNearGate(false);
                    break;
                }
            }
            //Reset the Current Gate
            foreach (PlayerBaseAI playerBaseAi in playerBaseAis)
            {
                playerBaseAi.nearestGate = null;
                playerBaseAi.StartNavigation();
            }
        }
    }

    void InitializePlayerUnits()
    {
        _playerGameObjectList.Clear();
        _playerAIList.Clear();
        foreach (Transform childTransform in playerUnits.transform)
        {
            GameObject playerGameObject = childTransform.gameObject;
            PlayerBaseAI playerBaseAI = playerGameObject.GetComponent<PlayerBaseAI>();
            _playerGameObjectList.Add(playerGameObject);
            _playerAIList.Add(playerBaseAI);
        }
    }

    void InitializeEnemyUnits()
    {
        _enemyGameObjectList.Clear();
        _enemyAIList.Clear();
        foreach (Transform childTransform in enemyUnits.transform)
        {
            GameObject enemyGameObject = childTransform.gameObject;
            EnemyBaseAI enemyBaseAI = enemyGameObject.GetComponent<EnemyBaseAI>();
            _enemyGameObjectList.Add(enemyGameObject);
            _enemyAIList.Add(enemyBaseAI);
        }
    }
    
    public Vector3 GetMidPoint()
    {
        if(playerUnits.transform.childCount == 0)
            return Vector3.zero;
            
        float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;
        int count = 0;
        foreach(Transform childTransform in playerUnits.transform)
        {
            totalX += childTransform.position.x;
            totalY += childTransform.position.y;
            totalZ += childTransform.position.z;
            count++;
        }
        float centerX = totalX / (float)count;
        float centerY = totalY / (float)count;
        float centerZ = totalZ / (float) count;
        Vector3 midPoint = new Vector3(centerX, centerY, centerZ);
        return midPoint;
    }

}