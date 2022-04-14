using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    public Drums drum;

    [SerializeField]
    private List<DrumBeatUI> DrumBeatUIList;

    public void SetDrum(Drums inDrum)
    {
        HideAllDrum();
        drum = inDrum;
        switch (inDrum)
        {
            case Drums.LeftShoulder:
                DrumBeatUIList[0].SetDrum(inDrum);
                break;
            case Drums.RightShoulder:
                DrumBeatUIList[1].SetDrum(inDrum);
                break;
            case Drums.LeftStomach:
                //DrumBeatUIList[2].SetDrum(inDrum);
                break;
            case Drums.RightStomach:
                //DrumBeatUIList[3].SetDrum(inDrum);
                break;
            case Drums.LeftThigh:
                DrumBeatUIList[4].SetDrum(inDrum);
                break;
            case Drums.RightThigh:
                DrumBeatUIList[5].SetDrum(inDrum);
                break;
        }
    }

    public void SetGrade(Grade inGrade)
    {
        HideGrade();

        switch (drum)
        {
            case Drums.LeftShoulder:
                DrumBeatUIList[0].SetGrade(inGrade);
                break;
            case Drums.RightShoulder:
                DrumBeatUIList[1].SetGrade(inGrade);
                break;
            case Drums.LeftStomach:
                //DrumBeatUIList[2].SetGrade(inGrade);
                break;
            case Drums.RightStomach:
                //DrumBeatUIList[3].SetGrade(inGrade);
                break;
            case Drums.LeftThigh:
                DrumBeatUIList[4].SetGrade(inGrade);
                break;
            case Drums.RightThigh:
                DrumBeatUIList[5].SetGrade(inGrade);
                break;
        }
    }

    public void HideAll()
    {
        HideAllDrum();
        HideGrade();
    }

    public void HideGrade()
    {
        foreach (var drumBeatUi in DrumBeatUIList)
        {
            drumBeatUi.HideGrade();
        }
    }

    public void HideAllDrum()
    {
        foreach (DrumBeatUI drumBeatUi in DrumBeatUIList)
        {
            drumBeatUi.HideDrum();
        }
    }

    void Awake()
    {
        SetDrum(drum);
        HideGrade();
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
