using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;








/*
    Button 0: Left Shoulder         [Pin 9]
    Button 1: Right Shoulder        [Pin 8]
    Button 2: Stomach               [Pin 7]
    Button 3: Left thigh            [Pin 6]
    Button 4: Right thigh           [Pin 5]
    Button 5: Pedal                 [Pin 4]
 */





public class InputManager : MonoBehaviour
{

    public List<AudioClip> audioClips;

    private static List<bool[]> _buttons;
    private static SerialPort _serialPort;
    private static Thread _readThread;
    private static bool _continue;


    // Start is called before the first frame update
    void Start()
    {
        _buttons = new List<bool[]>();

        _serialPort = new SerialPort();
        _serialPort.PortName = "COM3";
        _serialPort.BaudRate = 9600;
        _serialPort.Open();

        foreach (string s in SerialPort.GetPortNames())
        {
            print("\t" + s);
        }

        _continue = true;
        _readThread = new Thread(ReadPort);
        _readThread.Start();
    }

    private void Update()
    {
        bool[] buttonCalls = new bool[6];

        foreach (bool[] b in _buttons)
        {
            for (int i = 0; i < 6; i++)
            {
                buttonCalls[i] = buttonCalls[i] || b[i];
            }
        }
        _buttons.Clear();


        for (int i = 0; i < 6; i++)
        {
            if (buttonCalls[i])
            {
                if (i == 5)
                    continue;

                AudioSource sound = gameObject.AddComponent<AudioSource>();
                sound.clip = audioClips[i];
                sound.Play();
            }
        }
    }

    void ReadPort()
    {
        while (_continue)
        {
            try
            {
                string[] vals = _serialPort.ReadLine().Split(',');

                // sometimes we get garbage, throw it away
                if (vals.Length != 6)
                    continue;

                bool[] buttonVals = new bool[6];
                for (int i=0; i<vals.Length; i++)
                {
                    buttonVals[i] = vals[i] == "0";
                }
                _buttons.Add(buttonVals);
            }
            catch (TimeoutException) { }
        }
    }

    void OnDestroy()
    {
        _continue = false;
        _readThread.Join();
        _serialPort.Close();
    }
}
