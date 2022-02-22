using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ComboNote
{
    public char note;
    public int beat;
}

[CreateAssetMenu(fileName = "Combo", menuName = "Combo")]
public class Combo : ScriptableObject
{
    public List<ComboNote> comboOrder;
    public ComboEffect effect;
    public bool affectsAllies;
    public bool affectsEnemies;
}
