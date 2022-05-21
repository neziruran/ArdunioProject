using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine.UI;


public class ArdunioScript : MonoBehaviour
{
    private string port = "COM4";
    private int baudrate = 9600;
    private SerialPort ardunioPort;
    public bool isStreaming = false;

    
    public int lightSensorValue;
    public int micValue = 0;
    public int directionInput = 0;

    private void Awake()
    {
        OpenConnection();
    }
    

    void Update()
    {
        Debug.Log("Streaming Status : " + isStreaming);

        if (isStreaming)
        {
            GetInputs();
        }

    }
   
    public string[] splitArray;
    public char seperator;

    private void GetInputs()
    {
        string value = ReadSerialPort();
        var strings = value.Split(seperator);
        splitArray = strings.ToArray();

        directionInput = Int32.Parse(splitArray[0]);
        micValue = Int32.Parse(splitArray[1]);
        lightSensorValue = Int32.Parse(splitArray[2]);
    }
    
    string ReadSerialPort(int timeout = 50)
    {
        if (isStreaming)
        {
            string message;
            ardunioPort.ReadTimeout = timeout;
            message = ardunioPort.ReadLine();
            return message;
        }
        else
        {
            return "port is not open";
        }
    }
    
    
    void OnDestroy()
    {
        CloseConnection();
    }

    
 

    

    public void OpenConnection()
    {
        isStreaming = true;

        ardunioPort = new SerialPort(port, baudrate);
        ardunioPort.ReadTimeout = 100;
        ardunioPort.Open();
    }

    public void CloseConnection()
    {
        isStreaming = false;
        ardunioPort.Close();
    }

}