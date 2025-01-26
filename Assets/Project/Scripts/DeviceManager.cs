using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

public class DeviceManager : MonoBehaviour
{
    private Vector2 normalizedMousePosition = Vector2.zero;
    public string portName = "COM6";
    SerialPort serialPort;

     void Awake()
    {
        serialPort = new SerialPort(portName, 9600);
    }

    // Start is called before the first frame update
    void Start()
    {
        serialPort.ReadTimeout = 1000; // Set timeout to 1 second
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMouse();

        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0) // Check if data is available
                {
                    string data = serialPort.ReadLine();
                    if (!string.IsNullOrEmpty(data))
                    {
                        Debug.Log("Received: " + data);

                        string[] values = data.Split('|');
                        if (values.Length == 3)
                        {
                            int val1 = int.Parse(values[0]);
                            int val2 = int.Parse(values[1]);
                            int val3 = int.Parse(values[2]);

                            // Use val1, val2, and val3 as needed in Unity
                            Debug.Log($"val1: {val1}, val2: {val2}, val3: {val3}");
                        }
                    }
                }
            }
            catch (TimeoutException)
            {
                Debug.LogWarning("Timeout: No data received from Arduino.");
            }
        }
    }

    private void UpdateMouse(){
        Vector3 mousePosition = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        this.normalizedMousePosition = new Vector2(mousePosition.x / screenWidth, mousePosition.y / screenHeight);
    }

    public Vector2 GetMousePositionNormalized() {
        return this.normalizedMousePosition;
    }
}
