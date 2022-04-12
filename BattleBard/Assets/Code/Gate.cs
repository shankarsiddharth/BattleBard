using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(BoxCollider))]
public class Gate : MonoBehaviour
{
    public int health = 3;
    public BoxCollider coll;
    public GameObject[] spikes;
    public VisualEffect vfx;

    private void Start()
    {
        vfx.enabled = false;
        coll = GetComponent<BoxCollider>();    
    }

    private void Destroy()
    {
        //TODO: Add animation to gate destruction
        StartCoroutine(Destruction());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy();
        }
    }
    IEnumerator Destruction()
    {
        vfx.enabled = true;
        yield return new WaitForSeconds(2f);
        GameEvents.Instance.OnGateDestroyed(this);
        Destroy(gameObject);
    }

    void ShowOrHideSpike(bool show)
    {
        foreach (GameObject spike in spikes)
        {
            spike.SetActive(show);
        }
    }
}
