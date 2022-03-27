using System;
using System.Collections.Generic;

[Serializable]
public class Stats
{
	public float maxHealth;
	public float damage;
	public float movementSpeed;
	public float attackSpeed;
	public float range;
	public float armor;

	[UnityEngine.HideInInspector]
	public List<StatusEffect> currentEffects = new List<StatusEffect>();

	public static Stats operator *(Stats lhs, Stats rhs)
	{
		return new Stats
		{
			maxHealth		= lhs.maxHealth * rhs.maxHealth,
			damage			= lhs.damage * rhs.damage,
			movementSpeed	= lhs.movementSpeed * rhs.movementSpeed,
			attackSpeed		= lhs.attackSpeed * rhs.attackSpeed,
			range			= lhs.range * rhs.range,
			armor			= lhs.armor * rhs.armor
		};
	}
	public static Stats operator /(Stats lhs, Stats rhs)
	{
		return new Stats
		{
			maxHealth = lhs.maxHealth / rhs.maxHealth,
			damage = lhs.damage / rhs.damage,
			movementSpeed = lhs.movementSpeed / rhs.movementSpeed,
			attackSpeed = lhs.attackSpeed / rhs.attackSpeed,
			range = lhs.range / rhs.range,
			armor = lhs.armor / rhs.armor
		};
	}


}