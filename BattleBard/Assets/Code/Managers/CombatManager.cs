using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [HideInInspector]
    public List<Orc> enemyUnits;

    [HideInInspector]
    public List<Dwarf> playerUnits;

    private void Start()
    {
        playerUnits = FindObjectsOfType<Dwarf>().ToList();
        enemyUnits = FindObjectsOfType<Orc>().ToList();
    }
}