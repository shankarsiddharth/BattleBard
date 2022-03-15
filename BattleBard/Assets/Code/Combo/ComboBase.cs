using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ComboBase : MonoBehaviour 
{
	public bool affectsAllies;
	public bool affectsEnemies;

	public CombatManager _combatManager;
	protected void Awake()
	{
		_combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
		print(_combatManager);
	}

	public abstract void ComboPlayed(ComboBase effect, int level, Vector3 pos);
}
