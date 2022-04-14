using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ComboManager : MonoBehaviour
{
    public List<Combo> validCombos;
    public List<Note> drumsHit;

    private Metronome _metronome;
    private List<Combo> _defaultCombos;

    public bool playingCombo;
    private int _startBeat;

    private void Start()
    {
        GameEvents.Instance.onDrumPlayed.AddListener(OnDrumPlay);
        GameEvents.Instance.onDrumComboCompleted.AddListener(ListenForCombos);


        if (!gameObject.TryGetComponent(out _metronome))
        {
            Debug.LogWarning("The primary metronome should be attached to the same game object as the combo manager.");
        }

        _defaultCombos = new List<Combo>(validCombos);
        drumsHit = new List<Note>();
    }

    private void Update()
    {

        if (playingCombo)
        {
            // List of combos to remove because they are no longer valid
            List<Combo> invalidCombos = new List<Combo>();

            // Get current beat in combo
            float curBeat = _metronome.GetClosestBeat() - _startBeat;


            foreach (Combo validCombo in validCombos)
            {
                if (drumsHit.Count == 0)
                    break;

                ComboNote properComboNote = validCombo.comboOrder[drumsHit.Count];

                // Check if we are past the window to play the necessary combo note
                if (properComboNote.beat + _metronome.noteAccuracy < curBeat)
                {
                    invalidCombos.Add(validCombo);
                    print("Time ran out.");
                }
            }

            // Remove invalid combos
            foreach (Combo combo in invalidCombos)
            {
                validCombos.Remove(combo);
            }


            // All combos failed, reset
            if (validCombos.Count == 0)
            {
                ResetCombo();
            }
        }
    }

    private void ResetCombo()
	{
        playingCombo = false;
        validCombos = new List<Combo>(_defaultCombos);
        drumsHit.Clear();
    }

    private int GetComboLevel(Combo combo)
    {
        // Level values
        float level2 = 1;
        float level3 = 2;

        float total = 0;

        foreach (Note drumHit in drumsHit)
        {
            total += GetNoteValue(drumHit);
        }

        if (total / combo.comboOrder.Count > level2)
            return 1;

        if (total / combo.comboOrder.Count > level3)
            return 2;

        return 0;
    }

    private float GetNoteValue(Note n)
    {
        float GoodValue = 0;
        float GreatValue = 1;
        float PerfectValue = 2;

        return n.grade switch
        {
            Grade.Perfect => PerfectValue,
            Grade.Great => GreatValue,
            Grade.Good => GoodValue,
            _ => -1
        };
    }

    private void OnDrumPlay(Drums drum)
    {
        if (!playingCombo)
        {
            // Set base beat count from metronome beat
            _startBeat = _metronome.GetLastBeatCount();
            playingCombo = true;
        }

        Note playedNote = new Note { notePlayed = drum };

        // Always evaluate notes, even improv notes
        NoteEvaluator.EvaluateNote(ref playedNote);

        GameEvents.Instance.OnNoteEvaluated(playedNote);

        // List of combos to remove because they are no longer valid
        List<Combo> invalidCombos = new List<Combo>();

        drumsHit.Add(playedNote);

        // Check if note was a note for each valid combo
        foreach (Combo combo in validCombos)
        {
            ComboNote properComboNote = combo.comboOrder[drumsHit.Count - 1];

            // If the notes aren't the same, skip
            if (properComboNote.note != playedNote.notePlayed)
            {
                invalidCombos.Add(combo);
                continue;
            }

            // If they aren't on the same beat, skip
            if (properComboNote.beat != playedNote.timestamp - _startBeat)
            {
                print("Expected: " + properComboNote.beat);
                print("Got: " + (playedNote.timestamp - _startBeat));
                invalidCombos.Add(combo);
                continue;
            }

            // If its grade is too low, skip (or fail?)
            if (playedNote.grade == Grade.Bad)
            {
                print("Bad note!");
                invalidCombos.Add(combo);
                continue;
            }

            // If it was all good, then advance progress (and 'use' the combo if complete)
            if (drumsHit.Count == combo.comboOrder.Count)
			{
                // Get the level of the combo
                int level = GetComboLevel(combo);

                // Call the event then reset
                GameEvents.Instance.OnDrumComboCompleted(combo.effect, level, Vector3.zero);
                ResetCombo();
                return;
			}
        }

        
        // Remove invalid combos
        foreach (Combo combo in invalidCombos)
        {
            validCombos.Remove(combo);
        }

        if (validCombos.Count == 0)
            ResetCombo();
    }


    private static void ListenForCombos(ComboBase effect, int level, Vector3 pos)
    {
        if (effect == null)
        {
            Debug.LogWarning("Effect is unassigned!");
            return;
        }

        ComboBase b = Instantiate(effect, pos, Quaternion.Euler(Vector3.zero));
        b.ComboPlayed(effect, level, pos);

        print("Combo complete: " + effect.ToString());
    }

}
