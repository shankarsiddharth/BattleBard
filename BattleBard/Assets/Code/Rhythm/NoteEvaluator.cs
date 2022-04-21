using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NoteEvaluator : MonoBehaviour
{
    static private Metronome metronome;
    
    /*static public Text buttonDisplay;
    static public Text gradeDisplay;
    static public Text offsetDisplay;*/

    static public float PerfectThreshold = 0.075f;
    static public float GreatThreshold = 0.1f;
    static public float GoodThreshold = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        //GameEvents.Instance.onDrumPlayed.AddListener(EvaluateNote);
        //metronome = GetComponent<Metronome>();
        metronome = GameObject.FindGameObjectWithTag("AnimMetronome").GetComponent<Metronome>();

        /*gradeDisplay = (Text)GameObject.Find("Grade Display").GetComponent<Text>();
        buttonDisplay = GameObject.Find("Button Display").GetComponent<Text>();
        offsetDisplay = GameObject.Find("Offset Display").GetComponent<Text>();*/

        /*PerfectThreshold += metronome.GetFudgeOffset();
        GreatThreshold += metronome.GetFudgeOffset();
        GoodThreshold += metronome.GetFudgeOffset();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void EvaluateNote(ref Note note)
    {        
        double beatOffset = metronome.GetBeatOffset();
        double absOffset = Math.Abs(beatOffset);
        
        note.timestamp = metronome.GetClosestBeat();
        //note.timestamp = metronome.GetLastBeatCount();
        //note.timestamp = Time.time;
        
        /*
        offsetDisplay.text = beatOffset.ToString();
        */
        
        if(absOffset < PerfectThreshold)
        {
            ShowGrade(Grade.Perfect);
            note.grade = Grade.Perfect;
        }
        else if(absOffset < GreatThreshold)
        {
            ShowGrade(Grade.Great);
            note.grade = Grade.Great;
        }
        else if(absOffset < GoodThreshold)
        {
            ShowGrade(Grade.Good);
            note.grade = Grade.Good;
        }
        else
        {
            ShowGrade(Grade.Bad);
            note.grade = Grade.Bad;
        }
        ShowButton(note.notePlayed);
    }
    
    static void ShowButton(Drums drum)
    {
        /*if (buttonDisplay != null)
        {
            buttonDisplay.text = drum.ToString();
        }*/
    }
    static void ShowGrade(Grade grade)
    {
        /*if (gradeDisplay != null)
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
        }*/
    }
}
