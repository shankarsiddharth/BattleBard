using UnityEngine;
using System.IO;
using System.IO.Ports;

public class ArduinoIOController : MonoBehaviour
{
    public static string CommunicationPort = "COM3";
    //public static SerialPort HWSerialPort;
    public static int BaudRate = 9600;

    public static SerialPort ArduinoSerialPort = new SerialPort(CommunicationPort, BaudRate);


    // Start is called before the first frame update
    void Start()
    {
        foreach (string portName in SerialPort.GetPortNames())
        {
            Debug.Log("Attempting to connect to: " + portName);
            ArduinoSerialPort.PortName = portName;
            ArduinoSerialPort.ReadTimeout = 8;
            ArduinoSerialPort.Open();
            try
            {
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
            {}
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
        
    }
}
