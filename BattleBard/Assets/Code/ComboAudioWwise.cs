using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAudioWwise : MonoBehaviour
{
    public AK.Wwise.Event comboAudioEvent;
    public AK.Wwise.State akState;

    void Awake()
    {
        GameEvents.Instance.onDrumComboCompleted.AddListener(ListenForCombos);        
    }

    public void ListenForCombos(ComboBase effect, int level, Vector3 position)
    {
        if(effect is SpeedCombo)
        {
            //AkSoundEngine.SetState("Tier1", "Attacking");
            akState.SetValue();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        comboAudioEvent.Post(gameObject);
        //AkSoundEngine.SetState("Tier1", "Attacking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
