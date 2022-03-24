using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Gate : MonoBehaviour
{
    public int health = 3;
    public BoxCollider coll;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();    
    }

    private void Destroy()
    {
        //TODO: Add animation to gate destruction
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameEvents.Instance.OnGateDestroyed(this);
            Destroy();
        }
    }
}
