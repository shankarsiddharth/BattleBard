using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GradeUI : MonoBehaviour
{
    public Grade grade;

    [SerializeField]
    private List<GameObject> GradeList;

    public void SetGrade(Grade inGrade)
    {
        HideAll();
        int index = (int)(inGrade);
        grade = inGrade;
        switch (inGrade)
        {
            case Grade.Perfect:
                index = 0;
                break;
            case Grade.Great:
                index = 1;
                break;
            case Grade.Good:
                index = 2;
                break;
            case Grade.Bad:
                index = 3;
                break;
        }
        GradeList[index].SetActive(true);
    }
    
    public void HideAll()
    {
        foreach (GameObject gradeGameObject in GradeList)
        {
            gradeGameObject.SetActive(false);
        }
    }
}
