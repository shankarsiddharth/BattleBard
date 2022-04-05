using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Orc
{
    public override void Attack()
    {
        animator.SetTrigger("attack");

        target.TakeDamage(stats.damage);
    }
}
