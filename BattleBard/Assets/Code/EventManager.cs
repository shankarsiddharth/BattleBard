using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{

	#region Animations
	public delegate void AttackAnimEvent(Minion m);
	public static event AttackAnimEvent OnAttackAnim;
	public static void RaiseAttackAnimEvent(Minion m)
	{
		OnAttackAnim?.Invoke(m);
	}

	public delegate void MinionDeathAnimEvent(Minion m);
	public static event MinionDeathAnimEvent OnMinionDeathAnim;
	public static void RaiseMinionDeathAnimEvent(Minion m)
	{
		OnMinionDeathAnim?.Invoke(m);
	}
	#endregion

	#region Effects
	public delegate void AreaEffectApplied(Effect eff, Vector3 center, float radius);
	public static event AreaEffectApplied OnAreaEffectApplied;
	public static void RaiseAreaEffectAppliedEvent(Effect eff, Vector3 center, float radius) {
		OnAreaEffectApplied?.Invoke(eff, center, radius);
	}

	public delegate void EffectApplied(Effect eff, Minion target);
	public static event EffectApplied OnEffectApplied;
	public static void RaiseEffectAppliedEvent(Effect eff, Minion target)
	{
		OnEffectApplied?.Invoke(Object.Instantiate(eff), target);
	}

	public delegate void EffectExpired(Effect eff);
	public static event EffectExpired OnEffectExpired;
	public static void RaiseEffectExpiredEvent(Effect eff)
	{
		OnEffectExpired?.Invoke(eff);
	}

	// A minion has left a persistent zone effect
	public delegate void ZoneEffectExpired(Effect eff, Minion m);
	public static event ZoneEffectExpired OnZoneEffectExpired;
	public static void RaiseZoneEffectExpired(Effect eff, Minion m)
	{
		OnZoneEffectExpired?.Invoke(eff, m);
	}

	// A combo has just been hit and the current lane is now being effected
	public delegate void LaneComboComplete(Effect eff, Lane lane, bool affectsAllies, bool affectsEnemies);
	public static event LaneComboComplete OnLaneComboComplete;
	public static void RaiseLaneComboComplete(Effect eff, Lane lane, bool affectsAllies, bool affectsEnemies)
	{
		OnLaneComboComplete?.Invoke(eff, lane, affectsAllies, affectsEnemies);
	}
	#endregion

	#region Minions
	public delegate void AttackEvent(Minion attacker, Minion other);
	public static event AttackEvent OnMinionAttack;
	public static void RaiseMinionAttackEvent(Minion attacker, Minion other)
	{
		OnMinionAttack?.Invoke(attacker, other);
	}

	public delegate void MinionDeathEvent(Minion m);
	public static event MinionDeathEvent OnMinionDeath;
	public static void RaiseMinionDeathEvent(Minion m) {
		OnMinionDeath?.Invoke(m);
	}
	#endregion

	#region Lane Spawning
	public delegate void LaneSpawnEvent(int waveCount);
	public static event LaneSpawnEvent OnLaneSpawnEvent;
	public static void RaiseLaneSpawnEvent(int waveCount)
	{
		OnLaneSpawnEvent?.Invoke(waveCount);
	}
	#endregion

	#region Camera
	// Forces the camera to move to a lane. When the camera moves, fires CameraMovedEvent
	public delegate void ForceCameraMoveEvent(int lane);
	public static event ForceCameraMoveEvent OnForceCameraMovement;
	public static void RaiseForceCameraMovement(int lane) {
		OnForceCameraMovement?.Invoke(lane);
	}

	// After the camera has begun moving to a lane
	public delegate void CameraMovedEvent(int lane);
	public static event CameraMovedEvent OnCameraMove;
	public static void RaiseCameraMovedEvent(int lane)
	{
		OnCameraMove?.Invoke(lane);
	}
	#endregion

	#region Drums

	public enum Drum
	{
		LeftShoulder,
		RightShoulder,
		Stomach,
		LeftThigh,
		RightThigh
	}
	public delegate void DrumPlayed(Drum drum);
	public static event DrumPlayed OnDrumPlayed;

	public static void RaiseDrumPlayed(Drum drum)
	{
		OnDrumPlayed?.Invoke(drum);
	}

	#endregion
}
