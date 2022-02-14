using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

	#region Drums

	public enum Drum
	{
		LeftShoulder,
		RightShoulder,
		Stomach,
		LeftThigh,
		RightThigh,
		Pedal
	}
	public delegate void DrumPlayed(Drum drum);
	public static event DrumPlayed OnDrumPlayed;

	public static void RaiseDrumPlayed(Drum drum)
	{
		OnDrumPlayed?.Invoke(drum);
	}

	#endregion

	#region Combo
	public delegate void ComboComplete(ComboEffect effect, Vector3 position, bool affectsAllies, bool affectsEnemies);
	public static event ComboComplete OnComboComplete;

	public static void RaiseComboComplete(ComboEffect effect, Vector3 position, bool affectsAllies, bool affectsEnemies)
	{
		OnComboComplete?.Invoke(effect, position, affectsAllies, affectsEnemies);
	}
	#endregion

	// To be replaced or implemented
	public static void RaiseStatusEffectApplied(StatusEffect effect, Minion m) { Debug.Log(m.ToString() + " got " + effect.ToString()); }
}
