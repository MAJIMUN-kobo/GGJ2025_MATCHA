using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO.Ports;

public class DeviceManager : MonoBehaviour
{
    private Vector2 normalizedMousePosition = Vector2.zero;
    private Vector2 normalizedChasenPosition = Vector2.zero;
    public string portName = "COM6";
    SerialPort serialPort;

    private float radius = 0.5f; // Radius of the circle
    private Vector2 center;


    private Vector2 pointA;
    private Vector2 pointB;

    private Vector2 pointC;

    private Vector2 midAB;
    private Vector2 midAC;

    private Vector2 midBC;
    

     void Awake()
    {
        serialPort = new SerialPort(portName, 9600);
    }

    // Start is called before the first frame update
    void Start()
    {
        serialPort.ReadTimeout = 1000; // Set timeout to 1 second
        serialPort.Open();

        // Set the center of the circle to the center of the screen
        center = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // Calculate the vertices of the triangle
        pointA = CalculatePoint(center, radius, 90f);  // Top point
        pointB = CalculatePoint(center, radius, 210f); // Bottom left
        pointC = CalculatePoint(center, radius, 330f); // Bottom right

        // Calculate midpoints on the circle
        midAB = CalculateMidPointOnCircle(center, radius, 90f, 210f);
        midAC = CalculateMidPointOnCircle(center, radius, 90f, 330f);
        midBC = CalculateMidPointOnCircle(center, radius, 210f, 330f);

        // Log the points for debugging
        Debug.Log($"A: {pointA}, B: {pointB}, C: {pointC}");
        Debug.Log($"MidAB: {midAB}, MidAC: {midAC}, MidBC: {midBC}");
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
                        //Debug.Log("Received: " + data);

                        string[] values = data.Split('|');
                        if (values.Length == 3)
                        {
                            try
                            {
                                int val1 = int.Parse(values[0]);
                                int val2 = int.Parse(values[1]);
                                int val3 = int.Parse(values[2]);
                                this.normalizedChasenPosition = this.CalclateVector2(val1, val2, val3);

                                // Use val1, val2, and val3 as needed in Unity
                                //Debug.Log($"val1: {val1}, val2: {val2}, val3: {val3}");
                                // Debug.Log(this.normalizedChasenPosition);
                            }
                            catch (Exception ex)
                            {
                            } 
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

    private Vector2 CalculateMidPointOnCircle(Vector2 center, float radius, float angle1, float angle2)
    {
        // Normalize angles to avoid wrapping issues
        float midAngle = Mathf.Repeat((angle1 + angle2) / 2f, 360f);

        // Fix mid-angle if it's crossing the 360° boundary
        if (Mathf.Abs(angle2 - angle1) > 180f)
        {
            midAngle += 180f;
            if (midAngle >= 360f)
                midAngle -= 360f;
        }

        return CalculatePoint(center, radius, midAngle);
    }

    private Vector2 CalculatePoint(Vector2 center, float radius, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float x = center.x + radius * Mathf.Cos(angleRadians);
        float y = center.y + radius * Mathf.Sin(angleRadians);
        return new Vector2(x, y);
    }

    private Vector2 CalclateVector2(int val1, int val2, int val3){
        if (val1 == 1 && val2 == 0 && val3 == 0)
        {
            //A
            return this.pointA;
        }
        else if (val1 == 0 && val2 == 1 && val3 == 0)
        {
            //B
            return this.pointB;
        }
        else if (val1 == 0 && val2 == 0 && val3 == 1)
        {
            //C
            return this.pointC;
        }
        else if (val1 == 1 && val2 == 1 && val3 == 0)
        {
            //AB
            return this.midAB;
        }
        else if (val1 == 0 && val2 == 1 && val3 == 1)
        {
            //BC
            return this.midBC;
        }
        else if (val1 == 1 && val2 == 0 && val3 == 1)
        {
            //AC
            return this.midAC;
        }
        return this.center;
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

    public Vector2 GetChasenPositionNormalized() {
        return this.normalizedChasenPosition;
    }
}
