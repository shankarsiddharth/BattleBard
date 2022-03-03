using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public enum VFXType
{
    Heal = 0,
    Speed = 1,
    Death = 2,
    DefenceBoost = 3,
    AttackBoost = 4,
    TakingDamage = 5,
}

public class VFXTest : MonoBehaviour
{
    public VisualEffectAsset[] vfxGraphs;
    public VFXType vfxType;
    public bool isOn = false;

    VisualEffect[] vfxObjs;
    bool setVFX = false;
    
    void Awake()
    {
        vfxObjs = gameObject.GetComponentsInChildren<VisualEffect>();
        if (vfxObjs != null) Debug.Log(vfxObjs.Length);
    }

    void Update()
    {
        if (isOn && !setVFX)
        {
            foreach (VisualEffect vfxObj in vfxObjs)
            {
                vfxObj.visualEffectAsset = vfxGraphs[(int)vfxType];
                vfxObj.Play();
            }
            setVFX = true;
        }
        else if( !isOn && setVFX)
        {
            foreach (VisualEffect vfxObj in vfxObjs)
            {
                vfxObj.Stop();
                setVFX = false;
            }
        }
    }
}