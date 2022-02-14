using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{

    public Stats minionStats;
    
    
    private List<StatusEffect> effects;


    // Start is called before the first frame update
    void Start()
    {
        effects = new List<StatusEffect>();
        EventManager.OnStatusEffectApplied += ApplyStatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyStatus(StatusEffect effect, Minion m, int level)
	{
        if (m != this)
            return;

        if (!effects.Contains(effect))
            effects.Add(effect);
	}
}
