using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBeatUI : MonoBehaviour
{
    [Header("Drum Config")]
    public Drums drum;

    [Header("References")]
    [SerializeField]
    private GradeUI gradeUI;
    [SerializeField]
    private DrumUI drumUI;

    void Awake()
    {
        if (gradeUI == null || drumUI == null)
        {
            throw new NullReferenceException("gradeUI or drumUI null in DrumBeatUI");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDrum(Drums inDrum)
    {
        drum = inDrum;
        drumUI.ShowDrum(drum);
    }

    public void HideGrade()
    {
        gradeUI.HideAll();
    }

    public void SetGrade(Grade inGrade)
    {
        gradeUI.SetGrade(inGrade);
    }

    public void HideDrum()
    {
        drumUI.HideAll();
    }
}
