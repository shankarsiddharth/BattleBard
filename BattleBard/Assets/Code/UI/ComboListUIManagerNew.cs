
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboListUIManagerNew : MonoBehaviour
{
    public int totalNumberOfValidCombos = 3;
    public List<ComboListUIElement> comboListLineUiList;

    private ComboManager _comboManager;

    void Awake()
    {
        GameObject rhythmGameObject = GameObject.FindGameObjectWithTag("RhythmManager");
        _comboManager = rhythmGameObject.GetComponent<ComboManager>();
        if (_comboManager == null)
        {
            throw new NullReferenceException("comboManager is null in ComboListUIManager");
        }

        totalNumberOfValidCombos = _comboManager.validCombos.Count;
    }

    void Update()
    {
        if (_comboManager.validCombos.Count == totalNumberOfValidCombos)
        {
            ShowAllComboList();
        }
        else if(_comboManager.validCombos.Count == 1)
        {
            ComboBase effect = _comboManager.validCombos[0].effect;
            if (effect is DamageCombo)
            {
                comboListLineUiList[0].SetVisiblility(0,true);
                comboListLineUiList[1].HideAll();
                comboListLineUiList[2].HideAll();
            }
            else if (effect is HealingCombo)
            {
                comboListLineUiList[0].SetVisiblility(1, true);
                comboListLineUiList[1].HideAll();
                comboListLineUiList[2].HideAll();
            }
            else if(effect is SpeedCombo)
            {
                comboListLineUiList[0].SetVisiblility(2, true);
                comboListLineUiList[1].HideAll();
                comboListLineUiList[2].HideAll();
            }
        }
    }

    void ShowAllComboList()
    {
        for (int index = 0; index < comboListLineUiList.Count; index++)
        {
            comboListLineUiList[index].SetVisiblility(index, true);
        }
    }
}
