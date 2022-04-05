using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXTestTakingDamage: MonoBehaviour
{
    public VisualEffect hurtFX;

    public void Hurt()
    {
        hurtFX.Play();
    }
}
