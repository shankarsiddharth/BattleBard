using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCombo : ComboBase
{
    public List<float> tierDuration;
    public List<float> tierInterval;
    public List<float> tierPercentageHealed;

    public List<GameObject> tierVFXPrefab;

    public override void ComboPlayed(ComboBase effect, int level, Vector3 pos)
    {
        if (effect.affectsAllies)
        {
            foreach (Dwarf dwarf in _combatManager.playerUnits)
            {
                dwarf.TryGetComponent(out Actor ai);
                if (!dwarf.CompareTag("PrisonerDwarf") && !dwarf.CompareTag("DeadActor"))
                    StartCoroutine(StartTimer(level, ai));
            }
        }
        if (effect.affectsEnemies)
        {
            foreach (Orc orc in _combatManager.enemyUnits)
            {
                orc.TryGetComponent(out Actor ai);
                StartCoroutine(StartTimer(level, ai));
            }
        }
    }

    IEnumerator StartTimer(int level, Actor minion)
    {
        GameObject vfx = Instantiate(tierVFXPrefab[level], minion.transform);
        float timeSpent = 0f;
        while (timeSpent < tierDuration[level] && minion != null)
        {
            minion.currentHealth = Mathf.Min(minion.currentHealth + minion.stats.maxHealth * tierPercentageHealed[level], minion.stats.maxHealth);
            yield return new WaitForSeconds(tierInterval[level]);
            timeSpent += tierInterval[level];
        }

        if (minion != null)
            Destroy(vfx);
    }
}

