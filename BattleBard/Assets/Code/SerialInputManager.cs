using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;
using System.Threading;



/*
    Button # | Name             | PIN #
    ---------------------------------------
    Button 0 | Left Shoulder    | [Pin 9]
    Button 1 | Right Shoulder   | [Pin 8]
    Button 2 | Stomach          | [Pin 7]
    Button 3 | Left thigh       | [Pin 6]
    Button 4 | Right thigh      | [Pin 5]
    Button 5 | Pedal            | [Pin 4]
 */                               


public class SerialInputManager : MonoBehaviour
{

    public List<AudioClip> audioClips;

    private static List<bool[]> _buttons;
    private static SerialPort _serialPort;
    private static Thread _readThread;
    private static bool _continue;

    // The state of the buttons on the last frame. Important for checking if the button is being held or pressed multiple times
    private static bool[] _lastButtonState;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize with all false
        _lastButtonState = new bool[6];
        
        _buttons = new List<bool[]>();

        // Set up our Serial port to the arduino
        _serialPort = new SerialPort();
        _serialPort.PortName = "COM3";
        _serialPort.BaudRate = 9600;
        _serialPort.Open();

        // Print available ports (in case it didn't work the first time...
        foreach (string s in SerialPort.GetPortNames())
        {
            print("\t" + s);
        }

        _continue = true;

        // Split a thread for handling input from arduino
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
            // If the button is held
            if (buttonCalls[i])
            {
                // And it wasn't activated last frame
                if (buttonCalls[i] != _lastButtonState[i])
                {
                    // Raise the event
                    EventManager.RaiseDrumPlayed((EventManager.Drum) i);

                    /* Do we want to be explicit? If so, replace the above with this.
                    switch (i)
					{
                        case 0:
                            EventManager.RaiseDrumPlayed(EventManager.Drum.LeftShoulder);
                            break;
                        case 1:
                            EventManager.RaiseDrumPlayed(EventManager.Drum.RightShoulder);
                            break;
                        case 2:
                            EventManager.RaiseDrumPlayed(EventManager.Drum.Stomach);
                            break;
                        case 3:
                            EventManager.RaiseDrumPlayed(EventManager.Drum.LeftThigh);
                            break;
                        case 4:
                            EventManager.RaiseDrumPlayed(EventManager.Drum.RightThigh);
                            break;
                        case 5:
                            EventManager.RaiseDrumPlayed(EventManager.Drum.Pedal);
                            break;
					}
                    */
                }
            }
        }
    }

    // Runs on _readThread thread
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
