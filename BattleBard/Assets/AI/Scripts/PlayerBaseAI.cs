using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TPlayerType
{
    kOrcWarrior
};
public class PlayerBaseAI : MonoBehaviour
{
    public Stats stats;
    public TPlayerType PlayerType;
}
