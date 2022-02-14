using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
	public delegate void StatusEffectExpired(StatusEffect effect, Minion m, int level);
	public static event StatusEffectExpired OnStatusEffectExpired;

	public static void RaiseStatusEffectExpired(StatusEffect effect, Minion m, int level)
	{
		OnStatusEffectExpired?.Invoke(effect, m, level);
	}

	public delegate void StatusEffectApplied(StatusEffect effect, Minion m, int level);
	public static event StatusEffectApplied OnStatusEffectApplied;

	public static void RaiseStatusEffectApplied(StatusEffect effect, Minion m, int level)
	{
		OnStatusEffectApplied?.Invoke(effect, m, level);
	}
}
