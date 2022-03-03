using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FXTest_Die : MonoBehaviour
{
    public float dissolveTime = 1;
    
    GameObject orc;
    Material[] materials;
    VisualEffect dieVFX;

    private void Awake()
    {
        orc = transform.gameObject;
        materials = orc.GetComponent<MeshRenderer>().materials;
        dieVFX = orc.GetComponentInChildren<VisualEffect>();
        dieVFX.Stop();
    }

    void OnEnable()
    {
        StartCoroutine(Dissolve());
        StartCoroutine(DieVFX());
    }
    private void OnDisable()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("Dissolve", 0);
        }
    }

    IEnumerator Dissolve()
    {
        float time = 0;
        float dissolve = 0;
        float dissolveSmooth = dissolveTime * 24;
        float deltaTime = dissolveTime / dissolveSmooth;
        float deltaDissolve = 1 / dissolveSmooth;
        
        while (time < dissolveTime)
        {
            time += deltaTime;
            dissolve += deltaDissolve;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetFloat("Dissolve", dissolve);
            }
            yield return new WaitForSeconds(deltaTime);
        }
    }
    IEnumerator DieVFX()
    {
        //yield return new WaitForSeconds(0.05f);

        dieVFX.Play();
        yield return new WaitForSeconds(1.9f);
        dieVFX.Stop();
    }
}
