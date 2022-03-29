using UnityEngine;

public abstract class ComboBase : MonoBehaviour 
{
	public bool affectsAllies;
	public bool affectsEnemies;

	protected CombatManager _combatManager;

	protected void Awake()
	{
		_combatManager = FindObjectOfType<CombatManager>();
	}

	public abstract void ComboPlayed(ComboBase effect, int level, Vector3 pos);
}
