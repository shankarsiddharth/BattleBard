using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboListDrumUI : MonoBehaviour
{
    public Drums drum;

    [SerializeField] private List<GameObject> DrumList;

    public void ShowDrum(Drums inDrum)
    {
        HideAll();
        int index = (int)inDrum;
        drum = inDrum;
        switch (inDrum)
        {
            case Drums.LeftShoulder:
                index = 0;
                break;
            case Drums.RightShoulder:
                index = 1;
                break;
            case Drums.LeftStomach:
                index = 2;
                break;
            case Drums.RightStomach:
                index = 3;
                break;
            case Drums.LeftThigh:
                index = 4;
                break;
            case Drums.RightThigh:
                index = 5;
                break;
        }
        DrumList[index].SetActive(true);
    }
    
    public void HideAll()
    {
        foreach (GameObject drumGameObject in DrumList)
        {
            drumGameObject.SetActive(false);
        }
    }

    void Awake()
    {
        ShowDrum(drum);
    }
}
