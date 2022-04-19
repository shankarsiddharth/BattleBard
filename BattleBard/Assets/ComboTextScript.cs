using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum TUIEffectType
{
    kAttack,
    kHeal,
    kSpeed
}

public class ComboTextScript : MonoBehaviour
{
    public TUIEffectType UiEffectType;
    public Text uiText;
    private string text = "I";

    void Awake()
    {
        GameEvents.Instance.onDrumComboCompleted.AddListener(Test);
        uiText = GetComponent<Text>();
        uiText.text = text;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test(ComboBase effect, int level, Vector3 vec)
    {
        if (level == 0)
        {
            text = "I";
        }
        else if (level == 1)
        {
            text = "II";
        }
        else if (level == 2)
        {
            text = "III";
        }

        if (effect is SpeedCombo)
        {
            if (UiEffectType == TUIEffectType.kSpeed)
            {
                uiText.text = text;
            }
        }
        else if (effect is DamageCombo)
        {
            if (UiEffectType == TUIEffectType.kAttack)
            {
                uiText.text = text;
            }
        }
        else if (effect is HealingCombo)
        {
            if (UiEffectType == TUIEffectType.kHeal)
            {
                uiText.text = text;
            }
        }
        /*if (eff != null)
        {
            //GetComponent<Text>().text = eff.gameObject.name;
            GetComponent<Text>().SetAllDirty();
        }*/
    }
}
