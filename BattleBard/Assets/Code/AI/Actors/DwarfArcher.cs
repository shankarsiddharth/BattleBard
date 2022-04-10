using UnityEngine;

public class DwarfArcher : Dwarf
{
    [SerializeField] Arrow arrowPrefab;
   
    public override void Attack()
    {
        animator.SetFloat("attackAnimSpeed", (float) (3.4 * stats.attackSpeed));
        animator.SetTrigger("attack");
    }

    public void CreateArrow()
    {
        Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

        arrow.Initialize(this, target);
    }
}
