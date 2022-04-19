using UnityEngine;
using System.IO;
using System.IO.Ports;

public class ArduinoIOController : MonoBehaviour
{
    public static string CommunicationPort = "COM3";
    //public static SerialPort HWSerialPort;
    public static int BaudRate = 9600;

    public static SerialPort ArduinoSerialPort = new SerialPort(CommunicationPort, BaudRate);

    private void OnDestroy()
    {
        ArduinoSerialPort.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (string portName in SerialPort.GetPortNames())
        {
            Debug.Log("Attempting to connect to: " + portName);
            ArduinoSerialPort = new SerialPort(portName, BaudRate);
            ArduinoSerialPort.PortName = portName;
            ArduinoSerialPort.ReadTimeout = 8;
            try
            {
                ArduinoSerialPort.Open();
                ArduinoSerialPort.WriteLine("UNITY_HANDSHAKE");
                string handShakeRead = ArduinoSerialPort.ReadLine();
                if (!string.IsNullOrEmpty(handShakeRead) && handShakeRead == "ARDUINO_HANDSHAKE")
                {
                    Debug.Log("Connection successful");
                    break;
                }
                else
                {
                    Debug.Log("Connection failed");
                    ArduinoSerialPort.Close();
                }
            }
            catch
            { continue; }
        }
        if(!ArduinoSerialPort.IsOpen)
        {
            Debug.Log("No Serial Ports responded with correct handshake");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ArduinoSerialPort.IsOpen)
        {
            try
            {
                string readData = ArduinoSerialPort.ReadLine();
                if (string.IsNullOrEmpty(readData))
                { 
                    return;
                }

                Debug.Log("Recieved the following message from controller: " + readData);

                int buttonPressed;
                if(!int.TryParse(readData, out buttonPressed))
                {
                    Debug.Log("Unable to parse controller input to a number");
                }
                else
                {
                    Drums playedDrum;
                    switch(buttonPressed)
                    {
                        case 0:
                            playedDrum = Drums.LeftShoulder;
                            break;
                        case 1:
                            playedDrum = Drums.LeftThigh;
                            break;
                        case 2:
                            playedDrum = Drums.RightShoulder;
                            break;
                        case 3:
                            playedDrum = Drums.RightThigh;
                            break;
                        default:
                            throw new System.Exception("depricated button pressed");
                            break;
                    }
                    GameEvents.Instance.OnDrumPlayed(playedDrum);
                }
            }
            catch (System.Exception exception)
            {
                if(exception.Message != "The operation has timed out.")
                    Debug.Log("Caught the following exception while trying to read input: " + exception.Message);
            }
        }
    }
}
