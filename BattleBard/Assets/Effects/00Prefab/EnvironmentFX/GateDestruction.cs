using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GateDestruction : MonoBehaviour
{
    public GameObject[] spikes;
    public VisualEffect vfx;

    private void Awake()
    {
        vfx.enabled = false;
        ShowOrHideSpike(true);
    }

    private void OnEnable()
    {
        StartCoroutine(Destruction());
    }

    private void OnDisable()
    {
        ShowOrHideSpike(true);
        vfx.enabled = false;
    }

    IEnumerator Destruction()
    {
        vfx.enabled = true;
        yield return new WaitForSeconds(0.2f);

        ShowOrHideSpike(false);
    }

    void ShowOrHideSpike(bool show)
    {
        foreach (GameObject spike in spikes)
        {
            spike.SetActive(show);
        }
    }
}
