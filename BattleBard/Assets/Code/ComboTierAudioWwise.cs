using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTierAudioWwise : MonoBehaviour
{
    public int defaultTierLevel = 0;
    public int currentTierLevel = 0;
    public int currentComboType = 0;
    public AK.Wwise.Event comboTierAudioEvent;

    public AK.Wwise.State akIdleState;
    public List<AK.Wwise.State> akAttackState;
    public List<AK.Wwise.State> akHealState;
    public List<AK.Wwise.State> akSpeedState;

    void Awake()
    {
        GameEvents.Instance.onDrumComboCompleted.AddListener(ListenForCombos);        
    }

    public void ListenForCombos(ComboBase effect, int level, Vector3 position)
    {
        int tierLevel = level;
        currentTierLevel = tierLevel;
        if(effect is SpeedCombo)
        {
            akSpeedState[tierLevel].SetValue();
        }
        else if(effect is DamageCombo)
        {
            akAttackState[tierLevel].SetValue();
        }
        else if (effect is HealingCombo)
        {
            akHealState[tierLevel].SetValue();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //comboTierAudioEvent[currentTierLevel].Post(gameObject);
        comboTierAudioEvent.Post(gameObject);
        akIdleState.SetValue();
        //akIdleState[currentTierLevel].SetValue();
        //AkSoundEngine.SetState("Tier1", "Attacking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
