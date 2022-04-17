using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorUI: MonoBehaviour
{
    public GameObject AttackIndicatorGameObject;
    public GameObject HealIndicatorGameObject;
    public GameObject SpeedIndicatorGameObject;

    private List<int> _vfxEffectCount = new List<int> {0, 0, 0};

    private CombatManager _combatManager;

    void Awake()
    {
        GameObject combatManagerGameObject = GameObject.FindGameObjectWithTag("CombatManager");
        if (combatManagerGameObject == null)
        {
           throw new NullReferenceException("combatManagerGameObject is null in UIController");
        }

        _combatManager = combatManagerGameObject.GetComponent<CombatManager>();
        if (_combatManager == null)
        {
            throw new NullReferenceException("_combatManager is null in UIController");
        }

        GameEvents.Instance.onDrumComboCompleted.AddListener(OnDrumComboCompleted);

        AttackIndicatorGameObject.SetActive(false);
        HealIndicatorGameObject.SetActive(false);
        SpeedIndicatorGameObject.SetActive(false);

    }

    private void OnDrumComboCompleted(ComboBase effect, int level, Vector3 pos)
    {
        if (!transform.parent.gameObject.activeSelf)
        {
            return;
        }
        if (effect is DamageCombo)
        {
            AttackIndicatorGameObject.SetActive(true);
            _vfxEffectCount[0]++;
            DamageCombo damageCombo = (DamageCombo)effect;
            StatusEffect statusEffect = damageCombo.statusEffect;
            float durationInSeconds = statusEffect.tierDuration[level];
            StartCoroutine(StartVFXTimer(effect, durationInSeconds));
        }
        else if (effect is HealingCombo)
        {
            HealIndicatorGameObject.SetActive(true);
            _vfxEffectCount[1]++;
            //TODO: Track the time of the effect
            HealingCombo healingCombo = (HealingCombo) effect;
            float durationInSeconds = healingCombo.tierDuration[level];
            StartCoroutine(StartVFXTimer(effect, durationInSeconds));
        }
        else if (effect is SpeedCombo)
        {
            SpeedIndicatorGameObject.SetActive(true);
            _vfxEffectCount[2]++;
            SpeedCombo speedCombo = (SpeedCombo) effect;
            StatusEffect statusEffect = speedCombo.statusEffect;
            float durationInSeconds = statusEffect.tierDuration[level];
            StartCoroutine(StartVFXTimer(effect, durationInSeconds));
        }
    }

    public IEnumerator StartVFXTimer(ComboBase effect, float durationInSeconds)
    {
        
        Debug.Log("VFX Started");
        if (effect is DamageCombo)
        {
            yield return new WaitForSeconds(durationInSeconds);
            _vfxEffectCount[0]--;
            if (_vfxEffectCount[0] <= 0)
            {
                AttackIndicatorGameObject.SetActive(false);
            }
        }
        else if (effect is HealingCombo)
        {
            yield return new WaitForSeconds(durationInSeconds);
            _vfxEffectCount[1]--;
            if (_vfxEffectCount[1] <= 0)
            {
                HealIndicatorGameObject.SetActive(false);
            }
        }
        else
        if (effect is SpeedCombo)
        {
            yield return new WaitForSeconds(durationInSeconds);
            _vfxEffectCount[2]--;
            if (_vfxEffectCount[2] <= 0)
            {
                SpeedIndicatorGameObject.SetActive(false);
            }
        }
        
        Debug.Log("VFX Ended");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
