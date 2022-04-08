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
    // Tracks the progress of current combos so we don't have to check each note very frame
    [SerializeField] private List<int> _comboProgress;

    private bool _playingCombo;
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
        _comboProgress = new List<int>(new int[validCombos.Count]);
        drumsHit = new List<Note>();
    }

    private void Update()
    {

        if (_playingCombo)
        {
            // List of combos to remove because they are no longer valid
            List<Combo> invalidCombos = new List<Combo>();

            // Get current beat in combo
            float curBeat = _metronome.GetClosestBeat() - _startBeat;


            // Check current note for all valid combos for 'missing' a beat note
            for (int comboInd = 0; comboInd < _comboProgress.Count; comboInd++)
            {
                ComboNote properComboNote = validCombos[comboInd].comboOrder[_comboProgress[comboInd]];

                // Check if we are past the window to play the necessary combo note
                if (properComboNote.beat + _metronome.noteAccuracy < curBeat)
                    invalidCombos.Add(validCombos[comboInd]);
            }


            // Remove invalid combos
            foreach (Combo combo in invalidCombos)
            {
                _comboProgress.RemoveAt(validCombos.IndexOf(combo));
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
        _playingCombo = false;
        validCombos = new List<Combo>(_defaultCombos);
        _comboProgress = new List<int>(new int[validCombos.Count]);
        drumsHit.Clear();
    }

    // Called when a combo has been played
    private void SetComboNotes(Combo combo)
    {
        int hitNoteInd = 0;
        foreach (ComboNote cn in combo.comboOrder)
		{
            for (;  hitNoteInd < drumsHit.Count; hitNoteInd++)
			{
                Note n = drumsHit[hitNoteInd];
                
                // If the notes aren't the same, skip
                if (cn.note != n.notePlayed)
                    continue;

                // If they aren't on the same beat, skip
                if (cn.beat != n.timestamp)
                    continue;

                // If its grade is too low, skip (or fail?)
                if (n.grade == Grade.Bad)
                    continue;

                // Otherwise, it has to be the combo note
                drumsHit[hitNoteInd] = new Note { grade = n.grade, isCombo = true, notePlayed = n.notePlayed, timestamp = n.timestamp };
            }
		}
	}

    private void OnDrumPlay(Drums drum)
    {
        Debug.Log(drum);
        if (!_playingCombo)
        {
            // Set base beat count from metronome beat
            _startBeat = _metronome.GetLastBeatCount();
            _playingCombo = true;
        }

        Note playedNote = new Note { notePlayed = drum };

        // Always evaluate notes, even improv notes
        NoteEvaluator.EvaluateNote(ref playedNote);


        // List of combos to remove because they are no longer valid
        //List<Combo> invalidCombos = new List<Combo>();

        // Check if note was a beat note for each valid combo
        for (int comboInd = 0; comboInd < _comboProgress.Count; comboInd++)
        {
            ComboNote properComboNote = validCombos[comboInd].comboOrder[_comboProgress[comboInd]];

            // If the notes aren't the same, skip
            if (properComboNote.note != playedNote.notePlayed)
                continue;

            // If they aren't on the same beat, skip
            if (properComboNote.beat != playedNote.timestamp - _startBeat)
                continue;

            // If its grade is too low, skip (or fail?)
            if (playedNote.grade == Grade.Bad)
                continue;

            // If it was all good, then advance progress (and 'use' the combo if complete)
            _comboProgress[comboInd]++;
            if (_comboProgress[comboInd] == validCombos[comboInd].comboOrder.Count)
			{
                // DO IT
                print("Played combo " + validCombos[comboInd]);

                // Assign what notes are integral for the combo to execute
                SetComboNotes(validCombos[comboInd]);

                // Call the event then reset
                GameEvents.Instance.OnDrumComboCompleted(validCombos[comboInd].effect, 0, Vector3.zero);
                ResetCombo();
			}
        }

        drumsHit.Add(playedNote);
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
