using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Arrow : MonoBehaviour
{
    Actor target;
    Actor owner;

    Rigidbody rb;

    public float projectileSpeed;
    public float lifetime;

    public void Initialize(Actor owner, Actor target)
    {
        rb = GetComponent<Rigidbody>();

        this.owner = owner;
        this.target = target;

        transform.LookAt(target.transform);

        rb.velocity = (target.transform.position - owner.transform.position) * projectileSpeed;

        Invoke(nameof(DestroySelf), lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor actor = other.GetComponent<Actor>();

        if (actor && actor == target)
        {
            target.TakeDamage(owner.stats.damage);
            DestroySelf();
        }
    }
    
    private void DestroySelf()
    {
        CancelInvoke();
        
        Destroy(gameObject);
    }
}
