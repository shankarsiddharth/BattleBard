using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NoteEvaluator : MonoBehaviour
{
    static private Metronome metronome;
    static public Text buttonDisplay;
    static public Text gradeDisplay;
    static public Text offsetDisplay;
    [SerializeField]
    static private float PerfectThreshold;
    [SerializeField]
    static private float GreatThreshold;
    [SerializeField]
    static private float GoodThreshold;

    // Start is called before the first frame update
    void Start()
    {
        //GameEvents.Instance.onDrumPlayed.AddListener(EvaluateNote);
        metronome = GetComponent<Metronome>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void EvaluateNote(Drums drum)
    {        
        double beatOffset = metronome.GetBeatOffset();
        double absOffset = Math.Abs(beatOffset);

        if(absOffset < PerfectThreshold)
        {
            ShowGrade(Grade.Perfect);
        }
        else if(absOffset < GreatThreshold)
        {
            ShowGrade(Grade.Great);
        }
        else if(absOffset < GoodThreshold)
        {
            ShowGrade(Grade.Good);
        }
        else
        {
            ShowGrade(Grade.Bad);
        }
        ShowButton(drum);
    }
    
    static void ShowButton(Drums drum)
    {
        if (buttonDisplay != null)
        {
            buttonDisplay.text = drum.ToString();
        }
    }
    static void ShowGrade(Grade grade)
    {
        if (gradeDisplay != null)
        {
            switch (grade)
            {
                case Grade.Perfect:
                    gradeDisplay.text = "PERFECT!";
                    gradeDisplay.color = new Color(0f, 1f, 0f);
                    break;
                case Grade.Great:
                    gradeDisplay.text = "GREAT!";
                    gradeDisplay.color = new Color(0f, 0f, 1f);
                    break;
                case Grade.Good:
                    gradeDisplay.text = "GOOD";
                    gradeDisplay.color = new Color(1f, 1f, 0f);
                    break;
                case Grade.Bad:
                    gradeDisplay.text = "Bad...";
                    gradeDisplay.color = new Color(1f, 0f, 0f);
                    break;
            }
        }
    }
}
