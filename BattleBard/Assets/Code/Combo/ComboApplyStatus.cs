using UnityEngine;

public class ComboApplyStatus : ComboBase
{
    public StatusEffect statusEffect;

    override public void ComboPlayed(ComboBase effect, int level, Vector3 pos)
    {
        if (effect.affectsAllies)
        {
            foreach (Dwarf dwarf in _combatManager.playerUnits)
            {
                dwarf.TryGetComponent(out Actor ai);
                if (!dwarf.CompareTag("DwarfPrisoner"))
                    StartCoroutine(statusEffect.StartTimer(level, ai));
            }
        }
    }
}
