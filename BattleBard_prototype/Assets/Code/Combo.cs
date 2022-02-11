using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Combo", order = 1)]
public class Combo : ScriptableObject
{
    public List<char> comboOrder;
    public Effect effect;
    public bool affectsAllies;
    public bool affectsEnemies;
}
