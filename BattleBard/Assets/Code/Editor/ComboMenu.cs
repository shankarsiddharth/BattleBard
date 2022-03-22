using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComboMenu
{
	static GameObject lastCreatedPrefab;
	static StatusEffect lastCreatedStatus;
	static Combo lastCreatedCombo;

	[MenuItem("Assets/Create/Create Status Effect Combo", priority = 10)]
	static void CreateStatusEffectCombo()
	{
		GameObject createdObj = new GameObject("RENAME_NewStatusCombo");
		ComboApplyStatus applyStatusComp = createdObj.AddComponent<ComboApplyStatus>();

		lastCreatedStatus = ScriptableObject.CreateInstance<StatusEffect>();
		lastCreatedCombo = ScriptableObject.CreateInstance<Combo>();
		AssetDatabase.CreateAsset(lastCreatedStatus, "Assets/Prefabs/StatusEffects/RENAME_NewStatusEffect.asset");
		AssetDatabase.CreateAsset(lastCreatedCombo, "Assets/Prefabs/Combos/RENAME_NewCombo.asset");

		applyStatusComp.statusEffect = lastCreatedStatus;

		string localPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefabs/Combos/" + createdObj.name + ".prefab");
		lastCreatedPrefab = PrefabUtility.SaveAsPrefabAsset(createdObj, localPath);

		lastCreatedCombo.effect = lastCreatedPrefab.GetComponent<ComboApplyStatus>();

		Object.DestroyImmediate(createdObj);
		Selection.activeObject = lastCreatedPrefab;
	}

	[MenuItem("CONTEXT/ComboApplyStatus/Go to last created Status Effect")]
	[MenuItem("CONTEXT/Combo/Go to last created Status Effect")]
	static void SelectLastCreatedStatus()
	{
		Selection.activeObject = lastCreatedStatus;
	}

	[MenuItem("CONTEXT/ComboApplyStatus/Go to last created Combo")]
	[MenuItem("CONTEXT/StatusEffect/Go to last created Combo")]
	static void SelectLastCreatedCombo()
	{
		Selection.activeObject = lastCreatedCombo;
	}

	[MenuItem("CONTEXT/StatusEffect/Go to last created Status Combo")]
	[MenuItem("CONTEXT/Combo/Go to last created Status Combo")]
	static void SelectLastCreatedPrefab()
	{
		Selection.activeObject = lastCreatedPrefab;
	}
}
