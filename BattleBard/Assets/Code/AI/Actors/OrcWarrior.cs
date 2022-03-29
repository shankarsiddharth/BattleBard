public class OrcWarrior : Orc
{
    private new void Start() => base.Start();

    public override void Attack()
    {
        animator.SetTrigger("attack");

        target.TakeDamage(stats.damage);
    }
}
