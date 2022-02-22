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

    private bool _playingCombo;
    private int _startBeat;

    private void Start()
    {
        //EventManager.OnDrumPlayed += OnDrumPlayed;
        if (gameObject.TryGetComponent(out _metronome))
        {
            Debug.LogWarning("The primary metronome should be attached to the same game object as the combo manager.");
        }

        _defaultCombos = new List<Combo>(validCombos);

    }

    private void Update()
    {
        // Check notes in combo against metronome
        foreach (Combo c in validCombos)
        {
            
        }

        if (validCombos.Count == 0)
        {
            // Combo failed
            _playingCombo = false;
            validCombos = new List<Combo>(_defaultCombos);
        }
    }

    private void OnDrumPlay(Drums drum)
    {
        if (!_playingCombo)
        {
            // Set base beat count from metronome beat
            _startBeat = _metronome.GetLastBeatCount();
            _playingCombo = true;
        }

        Note note = new Note { notePlayed = drum };

        // Always evaluate notes, even improv notes
        NoteEvaluator.EvaluateNote(note);

        drumsHit.Add(note);
    }

    /*
	void CheckCombo()
    {
        foreach (Combo validCombo in valid_combos)
        {
            List<char> reversedOrder = new List<char>(validCombo.comboOrder);
            reversedOrder.Reverse();

            if (reversedOrder.Count > drumsHit.Count)
                continue;

            List<char> sublist = drumsHit.GetRange(0, reversedOrder.Count);

            // If they aren't equal size, move to the next combo to check
            if (reversedOrder.Count != sublist.Count)
                continue;

            bool equals = true;
            for (int i=0; i<reversedOrder.Count; i++)
            {
                if (reversedOrder[i] != sublist[i])
                {
                    equals = false;
                    break;
                }
            }

            if (equals)
            {
				// Ugly, but get main camera's position
                //EventManager.RaiseComboComplete(validCombo.effect, Camera.main.transform.position, validCombo.affectsAllies, validCombo.affectsEnemies);

                // Carry over the last drum played
                char lastDrum = drumsHit[0];
                drumsHit.Clear();
                drumsHit.Add(lastDrum);
            }
        }
    }
    */


    /*
    #region Keyboard Events
    public void LeftThigh(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseDrumPlayed(EventManager.Drum.LeftThigh);
    }

    public void RightThigh(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseDrumPlayed(EventManager.Drum.RightThigh);
    }

    public void Stomach(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseDrumPlayed(EventManager.Drum.Stomach);
    }

    public void LeftShoulder(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseDrumPlayed(EventManager.Drum.LeftShoulder);
    }

    public void RightShoulder(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseDrumPlayed(EventManager.Drum.RightShoulder);
    }

    // TODO: This input needs to be managed elsewhere...
    public void Pedal(InputAction.CallbackContext context)
    {
        if (context.performed)
            EventManager.RaiseDrumPlayed(EventManager.Drum.Pedal);
    }
    #endregion

    #region Events
    private void OnDrumPlayed(EventManager.Drum drum)
	{
        print(drum);
		switch (drum)
		{
			case EventManager.Drum.LeftShoulder:
                drumsHit.Insert(0, '1');
                break;
			case EventManager.Drum.RightShoulder:
                drumsHit.Insert(0, '2');
                break;
			case EventManager.Drum.Stomach:
                drumsHit.Insert(0, '3');
                break;
			case EventManager.Drum.LeftThigh:
                drumsHit.Insert(0, '5');
                break;
			case EventManager.Drum.RightThigh:
                drumsHit.Insert(0, '4');
                break;
			case EventManager.Drum.Pedal:
				break;
		}
        CheckCombo();
	}
	#endregion*/
}
