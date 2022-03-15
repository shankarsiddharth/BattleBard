using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    public List<GameObject> GetPlayerGameObjectList()
    {
        List<GameObject> playerList = new List<GameObject>();
        GameObject newplayer = new GameObject();
        playerList.Add(newplayer);
        playerList.Add(newplayer);
        return playerList;
    }

    public List<GameObject> GetEnemyGameObjectList()
    {
        List<GameObject> enemyList = new List<GameObject>();
        GameObject newEnemy = new GameObject();
        enemyList.Add(newEnemy);
        enemyList.Add(newEnemy);
        return enemyList;
    }

    public List<PlayerBaseAI> GetPlayerBaseAIList()
    {
        List<PlayerBaseAI> playerList = new List<PlayerBaseAI>();
        PlayerBaseAI newplayer = new PlayerBaseAI();
        playerList.Add(newplayer);
        playerList.Add(newplayer);
        return playerList;
    }

    public List<EnemyBaseAI> GetEnemyBaseAIList()
    {
        List<EnemyBaseAI> enemyList = new List<EnemyBaseAI>();
        EnemyBaseAI newEnemy = new EnemyBaseAI();
        enemyList.Add(newEnemy);
        enemyList.Add(newEnemy);
        return enemyList;
    }

    private SortedDictionary<int, PlayerBaseAI> GetPlayer()
    {
        SortedDictionary<int, PlayerBaseAI> dict = new SortedDictionary<int, PlayerBaseAI>();
        PlayerBaseAI newplayer = new PlayerBaseAI();
        dict.Add(1, newplayer);
        dict.Add(2, newplayer);
        return dict;
    }
    
    private SortedDictionary<int, EnemyBaseAI> GetEnemies()
    {
        SortedDictionary<int, EnemyBaseAI> dict = new SortedDictionary<int, EnemyBaseAI>();
        EnemyBaseAI newEnemy = new EnemyBaseAI();
        dict.Add(1, newEnemy);
        dict.Add(2, newEnemy);
        return dict;
    }

    [Header("Values & References")]
    public string PlayerUnitTagString = "PlayerUnits";
    public string EnemyUnitTagString = "EnemyUnits";
    
    [Header("Autofill Values & References")]
    public GameObject PlayerUnits;
    public GameObject EnemyUnits;

    private void Awake()
    {
        PlayerUnits = GameObject.FindGameObjectWithTag(PlayerUnitTagString);
        EnemyUnits = GameObject.FindGameObjectWithTag(EnemyUnitTagString);

        if (PlayerUnits == null || EnemyUnits == null)
        {
            throw new NullReferenceException("Reference missing for PlayerUnits or EnemyUnits in CombatManager");
        }

        foreach (Transform childTransform in PlayerUnits.transform)
        {
            GameObject playerGameObject = childTransform.gameObject;
            PlayerBaseAI playerBaseAI = playerGameObject.GetComponent<PlayerBaseAI>();
            Stats stats = playerBaseAI.stats;
            stats.maxHealth = Random.Range(1, 5);
            stats.armor = Random.Range(1, 5);
            stats.damage = Random.Range(1, 5);
            playerBaseAI.stats = stats;
            playerBaseAI.currentHealth = stats.maxHealth;
        }
        
        foreach (Transform childTransform in EnemyUnits.transform)
        {
            GameObject enemyGameObject = childTransform.gameObject;
            EnemyBaseAI enemyBaseAI = enemyGameObject.GetComponent<EnemyBaseAI>();
            Stats stats = enemyBaseAI.stats;
            stats.maxHealth = Random.Range(1, 5);
            stats.armor = Random.Range(1, 5);
            stats.damage = Random.Range(1, 5);
            enemyBaseAI.stats = stats;
            enemyBaseAI.currentHealth = stats.maxHealth;
        }
    }
}
