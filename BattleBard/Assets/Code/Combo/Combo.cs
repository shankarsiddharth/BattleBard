using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Combo")]
public class Combo : ScriptableObject
{
    public List<char> comboOrder;
    public ComboEffect effect;
    public bool affectsAllies;
    public bool affectsEnemies;
}
