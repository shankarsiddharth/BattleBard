using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrumHitUIManager : MonoBehaviour
{
    public List<GameObject> _noteUIGameObjects;
    public List<NoteUI> _noteUIList;
    public Queue<Drums> drumPlayQueue;
    public Queue<Note> notePlayQueue;
    
    void Awake()
    {
        _noteUIGameObjects = new List<GameObject>();
        drumPlayQueue = new Queue<Drums>();
        notePlayQueue = new Queue<Note>();
        foreach (Transform child in gameObject.transform)
        {
            _noteUIGameObjects.Add(child.gameObject);
            _noteUIList.Add(child.gameObject.GetComponent<NoteUI>());
        }
        GameObject rhythmGameObject = GameObject.FindGameObjectWithTag("RhythmManager");
        //GameEvents.Instance.onDrumPlayed.AddListener(OnDrumPlayed);
        GameEvents.Instance.onNoteEvaluated.AddListener(OnNoteEvaluated);
        GameEvents.Instance.onDrumComboCompleted.AddListener(OnDrumComboCompleted);
    }

    private void OnNoteEvaluated(Note note)
    {
        //throw new NotImplementedException();
        if (notePlayQueue.Count == 0)
        {
            notePlayQueue.Enqueue(note);
            DisplayNoteUIOnDrumHit();
        }
        else if (notePlayQueue.Count == _noteUIGameObjects.Count)
        {
            notePlayQueue.Dequeue();
            notePlayQueue.Enqueue(note);
            DisplayNoteUIOnDrumHit();
        }
        else
        {
            notePlayQueue.Enqueue(note);
            DisplayNoteUIOnDrumHit();
        }
    }

    private void DisplayNoteUIOnDrumHit()
    {
        for (int index = 0; index < _noteUIList.Count; index++)
        {
            if (index < notePlayQueue.Count)
            {
                Note currentNote = notePlayQueue.ToArray()[index];
                _noteUIList[index].SetDrum(currentNote.notePlayed);
                _noteUIList[index].SetGrade(currentNote.grade);
            }
            else
            {
                _noteUIList[index].HideAll();
            }
        }
    }

    private void OnDrumComboCompleted(ComboBase effect, int level, Vector3 pos)
    {
        //throw new NotImplementedException();
        drumPlayQueue.Clear();
        notePlayQueue.Clear();
    }

    private void OnDrumPlayed(Drums drum)
    {
        //throw new NotImplementedException();
        if (drumPlayQueue.Count == 0)
        {
            drumPlayQueue.Enqueue(drum);
            DisplayDrumUIOnDrumHit();
        }
        else if (drumPlayQueue.Count == _noteUIGameObjects.Count)
        {
            drumPlayQueue.Dequeue();
            drumPlayQueue.Enqueue(drum);
            DisplayDrumUIOnDrumHit();
        }
        else
        {
            drumPlayQueue.Enqueue(drum);
            DisplayDrumUIOnDrumHit();
        }
    }

    private void DisplayDrumUIOnDrumHit()
    {
        for (int index = 0; index < _noteUIList.Count; index++)
        {
            if (index < drumPlayQueue.Count)
            {
                _noteUIList[index].SetDrum(drumPlayQueue.ToArray()[index]);
            }
            else
            {
                _noteUIList[index].HideAll();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (NoteUI noteUi in _noteUIList)
        {
            noteUi.HideAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
