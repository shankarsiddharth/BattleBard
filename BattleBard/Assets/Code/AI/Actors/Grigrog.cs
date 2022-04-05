using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grigrog : Orc
{
    private List<Dwarf> cleaveTargets = new List<Dwarf>();

    public override void Attack()
    {
        animator.SetTrigger("attack");
        foreach(Dwarf dwarf in cleaveTargets)
        {
            dwarf.TakeDamage(stats.damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Dwarf dwarf = other.GetComponent<Dwarf>();

        if (dwarf)
        {
            print("Dwarf Entered Cleave Area");
            cleaveTargets.Add(dwarf);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Dwarf dwarf = other.GetComponent<Dwarf>();

        if (dwarf)
        {
            cleaveTargets.Remove(dwarf);
        }
    }
}
