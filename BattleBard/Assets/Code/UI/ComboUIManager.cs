using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboUIManager : MonoBehaviour
{
    public GameObject NoteUIPrefab;
    public int posXFullBeatMultiple = 14;
    public int totalDrums = 6;
    public int totalNumberOfValidCombos = 3;

    private ComboManager _comboManager;
    private float _timeElapsed = 1.0f;
    private int _count = 0;

    private List<GameObject> _comboUIGameObjects;

    void Awake()
    {
        GameObject rhythmGameObject = GameObject.FindGameObjectWithTag("RhythmManager");
        _comboManager = rhythmGameObject.GetComponent<ComboManager>();
        if (_comboManager == null)
        {
            throw new NullReferenceException("comboManager is null in ComboUIManager");
        }

        totalNumberOfValidCombos = _comboManager.validCombos.Count;
        _comboUIGameObjects = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _comboUIGameObjects.Clear();
        DestroyAllChildren();
        if (_comboManager.validCombos.Count == totalNumberOfValidCombos)
        {
            return;
        }

        if (_comboManager.validCombos.Count == 0)
        {
            return;
        }

        //_comboUIGameObjects = new List<GameObject>();
        Combo validCombo = _comboManager.validCombos[0];
        foreach (ComboNote comboNote in validCombo.comboOrder)
        {
            GameObject noteUIGameObject = Instantiate(NoteUIPrefab, transform);
            RectTransform rectTransform = noteUIGameObject.GetComponent<RectTransform>();
            float beat = comboNote.beat;
            float difference = (float)(beat - Math.Floor(beat));
            if (difference <= 0f)
            {
                //Full Beat
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _count * posXFullBeatMultiple, rectTransform.rect.width);
                _count++;
            }
            else if (difference <= 0.5f)
            {
                //Half Beat
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _count * (posXFullBeatMultiple / 2.0f), rectTransform.rect.width);

            }
            NoteUI noteUI = noteUIGameObject.GetComponent<NoteUI>();
            noteUI.SetDrum(comboNote.note);
            _comboUIGameObjects.Add(noteUIGameObject);
        }
        _count = 0;

        foreach (Note note in _comboManager.drumsHit)
        {
            GameObject noteUIGameObject = Instantiate(NoteUIPrefab, transform);
            RectTransform rectTransform = noteUIGameObject.GetComponent<RectTransform>();
            float beat = note.timestamp;
            float difference = (float)(beat - Math.Floor(beat));
            if (difference <= 0f)
            {
                //Full Beat
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _count * posXFullBeatMultiple, rectTransform.rect.width);
                _count++;
            }
            /*else if (difference <= 0.25f)
            {
                //Quarter Beat
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _count * (posXFullBeatMultiple / 4.0f), rectTransform.rect.width);
            }*/
            else if (difference <= 0.5f)
            {
                //Half Beat
                rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _count * (posXFullBeatMultiple / 2.0f), rectTransform.rect.width);
            }
            NoteUI noteUI = noteUIGameObject.GetComponent<NoteUI>();
            noteUI.SetDrum(note.notePlayed);
            noteUI.SetGrade(note.grade);
        }

        _count = 0;

        /*for (int index = 0; index < _comboManager.drumsHit.Count; index++)
        {
            Note note = _comboManager.drumsHit[index];
            if (index <= _comboUIGameObjects.Count)
            {
                NoteUI noteUI = _comboUIGameObjects[index].GetComponent<NoteUI>();
                noteUI.SetGrade(note.grade);
            }
        }*/

        /*_timeElapsed += Time.deltaTime;
        if (_timeElapsed >= 1.0f)
        {
            _timeElapsed = 0;
            GameObject noteGameObject = Instantiate(NoteUIPrefab, transform);
            RectTransform rectTransform = noteGameObject.GetComponent<RectTransform>();
            //rectTransform.localPosition = new Vector3(_count * posXFullBeatMultiple, 0, 0);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _count * posXFullBeatMultiple, rectTransform.rect.width);
            NoteUI noteUI = noteGameObject.GetComponent<NoteUI>();
            noteUI.SetDrum((Drums) (_count % totalDrums));
            _count++;
        }*/
    }

    void DestroyAllChildren()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
