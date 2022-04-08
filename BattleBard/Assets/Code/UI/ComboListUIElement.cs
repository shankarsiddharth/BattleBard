using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ComboListUIElement : MonoBehaviour
{
    public List<GameObject> uiObjectList;

    [SerializeField] 
    private GameObject AttackGameObject;
    [SerializeField]
    private GameObject HealGameObject;
    [SerializeField]
    private GameObject SpeedGameObject;

    public void SetAttackVisibility(bool isVisible)
    {
        AttackGameObject.SetActive(isVisible);
    }
    public void SetHealVisibility(bool isVisible)
    {
        HealGameObject.SetActive(isVisible);
    }

    public void SetSpeedVisibility(bool isVisible)
    {
        SpeedGameObject.SetActive(isVisible);
    }

    public void SetVisiblility(int index, bool isVisible)
    {
        if(index >= uiObjectList.Count)
            throw new IndexOutOfRangeException("index is outofrange in SetVisiblility in ComboListUIElement");

        for (int i = 0; i < uiObjectList.Count; i++)
        {
            if (i == index)
            {
                uiObjectList[i].SetActive(isVisible);
            }
            else
            {
                uiObjectList[i].SetActive(!isVisible);
            }
        }
       
    }

    public void HideAll()
    {
        foreach (GameObject uiObject in uiObjectList)
        {
            uiObject.SetActive(false);
        }
    }
}
