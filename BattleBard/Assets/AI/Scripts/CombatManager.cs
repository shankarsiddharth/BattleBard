using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public SortedDictionary<int, PlayerBaseAI> GetPlayers()
    {
        SortedDictionary<int, PlayerBaseAI> dict = new SortedDictionary<int, PlayerBaseAI>();
        PlayerBaseAI newplayer = new PlayerBaseAI();
        dict.Add(1, newplayer);
        dict.Add(2, newplayer);
        return dict;
    }
}
