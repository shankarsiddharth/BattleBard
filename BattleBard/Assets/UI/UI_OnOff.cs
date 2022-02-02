using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnOff : MonoBehaviour
{
    GameObject shoulder_Left;
    GameObject shoulder_Right;
    GameObject stomach;
    GameObject thigh_Left;
    GameObject thigh_Right;
    GameObject pedal;


    void Start()
    {
        shoulder_Left = GameObject.Find("Shoulder_Left");
        shoulder_Right = GameObject.Find("Shoulder_Right");
        stomach = GameObject.Find("Stomach");
        thigh_Left = GameObject.Find("Thigh_Left");
        thigh_Right = GameObject.Find("Thigh_Right");
        pedal = GameObject.Find("Pedal");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("7"))
        {//Shoulder_Left
            shoulder_Left.SetActive(true);
        }
        if (Input.GetKeyUp("7"))
        {
            shoulder_Left.SetActive(false);
        }
        if (Input.GetKeyDown("9")) //Shoulder_Right
        {
            shoulder_Right.SetActive(true);
        }
        if (Input.GetKeyUp("9"))
        {
            shoulder_Right.SetActive(false);
        }
        if (Input.GetKeyDown("5")) //Stomach
        {
            stomach.SetActive(true);
        }
        if (Input.GetKeyUp("5"))
        {
            stomach.SetActive(false);
        }
        if (Input.GetKeyDown("1")) //thigh_L
        {
            thigh_Left.SetActive(true);
        }
        if (Input.GetKeyUp("1"))
        {
            thigh_Left.SetActive(false);
        }
        if (Input.GetKeyDown("3")) //thigh_R
        {
            thigh_Right.SetActive(true);
        }
        if (Input.GetKeyUp("3"))
        {
            thigh_Right.SetActive(false);
        }
    }
}
