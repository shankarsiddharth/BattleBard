using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXTest : MonoBehaviour
{
    public VisualEffect vfxTest;
    public float lifetime = 1;

    // Start is called before the first frame update
    void Start()
    {
        vfxTest.Play();
        StartCoroutine(VFXDisplay());
    }

    IEnumerator VFXDisplay()
    {
        float time = 0;

        yield return new WaitForSeconds(lifetime);
        vfxTest.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
