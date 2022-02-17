using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteEvaluator : MonoBehaviour
{

    private Metronome metronome;
    public Text buttonDisplay;
    public Text gradeDisplay;
    public Text offsetDisplay;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.onDrumPlayed += EvaluateNote;
        //buttonDisplay = GameObject.Find("Button Display").GetComponent<Text>();
        //gradeDisplay = GameObject.Find("Grade Display").GetComponent<Text>();
        //GameEvents.Instance.onDrumPlayed.AddListener(DrumPlayed);
        metronome = GetComponent<Metronome>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EvaluateNote(Drums drum)
    {
        buttonDisplay.text = drum.ToString();
        double beatOffset = metronome.GetBeatOffset();
        offsetDisplay.text = beatOffset.ToString();
        if(beatOffset < .05)
        {
            gradeDisplay.text = "PERFECT!";
            gradeDisplay.color = new Color(0f, 1f, 0f);
        }
        else if(beatOffset < .075)
        {
            gradeDisplay.text = "GREAT!";
            gradeDisplay.color = new Color(0f, 0f, 1f);
        }
        else if(beatOffset < .1)
        {
            gradeDisplay.text = "GOOD";
            gradeDisplay.color = new Color(1f, 1f, 0f);
        }
        else
        {
            gradeDisplay.text = "Bad...";
            gradeDisplay.color = new Color(1f, 0f, 0f);
        }
    }
}
