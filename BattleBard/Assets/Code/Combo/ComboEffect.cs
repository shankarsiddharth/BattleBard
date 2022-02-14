using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
    [Tooltip("If this combo has a custom effect tick this.")]
    public bool hasCustomEffect;

    [Header("Status Effect")]
    [Tooltip("Assign this to apply a status effect to all allies/enemies in lane")]
    public StatusEffect effectApplied;
    public bool appliesToEnemies;
    public bool appliesToAllies;


    // Statically initialize 
    private bool _staticInitialized;
	private void Awake()
	{
		if (!_staticInitialized)
		{
            _staticInitialized = true;
            EventManager.OnComboComplete += ListenForCombos;
            Destroy(gameObject);
        }
	}

    
    private static void ListenForCombos(ComboEffect effect, Vector3 pos, bool affectsAllies, bool affectsEnemies) 
    {
        if (effect.hasCustomEffect)
		{
            Instantiate(effect, pos, Quaternion.Euler(Vector3.zero));
		}

        if (effect.effectApplied != null) {
            if (effect.appliesToAllies) {
                // Get all allies and apply effect
                List<Minion> allies = null; // = gameState.GET_ALLIED_MINIONS();
                foreach (Minion m in allies) {
                    EventManager.RaiseStatusEffectApplied(effect.effectApplied, m);
                }
            }

            if (effect.appliesToEnemies) {
                // Get all allies and apply effect
                List<Minion> enemies = null; // = gameState.GET_ENEMY_MINIONS();
                foreach (Minion m in enemies) {
                    EventManager.RaiseStatusEffectApplied(effect.effectApplied, m);
                }
            }
        }

        print("Combo complete: " + effect.ToString());
    }

}
