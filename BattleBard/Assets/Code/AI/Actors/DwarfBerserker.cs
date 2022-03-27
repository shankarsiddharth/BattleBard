public class DwarfBerserker : Dwarf
{
    public override void Attack()
    {
        animator.SetTrigger("attack");

        target.TakeDamage(stats.damage);
    }
}
