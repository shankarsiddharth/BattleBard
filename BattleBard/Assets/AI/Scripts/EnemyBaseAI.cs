using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAI : BaseAI
{
    public TEnemyType EnemyType;

    new void Update()
    {
        base.Update();
        if (this.target == null && CombatManager.Instance._playerGameObjectList.Count != 0)
        {
            this.target = SearchTarget(CombatManager.Instance._playerGameObjectList);
        }
    }

}
