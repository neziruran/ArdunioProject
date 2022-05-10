using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Net;
using TMPro;
using UnityEngine.UI;
public class ArdunioMic : MonoBehaviour
{
    private string port = "COM4";
    private int baudrate = 9600;
    SerialPort ardunioPort;
    public bool isStreaming = false;
    public int micValue = 0;

    private void Awake()
    {
        OpenConnection();
    }


    void Update()
    {
        Debug.Log("Streaming Status : " + isStreaming);


        // connection handling
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isStreaming)
            {
                CloseConnection();
            }
        }

        if (isStreaming)
        {
            ReadLog();
            micValue = GetInput();
        }



    }

    void OnDestroy()
    {
        CloseConnection();
    }


    private void ReadLog()
    {
        string value = ReadSerialPort();
        if (value != null)
            Debug.Log(value);
    }

    int GetInput(int timeout = 50)
    {
        string message;
        ardunioPort.ReadTimeout = timeout;
        try
        {
            message = ardunioPort.ReadLine();
        }
        catch (System.TimeoutException e)
        {
            Debug.LogWarning(e.Message);
            return 0;
        }

        if (string.IsNullOrEmpty(message))
        {
            Debug.LogWarning("Message was null or empty.");
            return 0;
        }
        int inputValue;
        if (int.TryParse(message, out inputValue))
            return inputValue;
        else
        {
            Debug.LogWarning(message + " failed to parse.");
            return 0;
        }
    }
    

    string ReadSerialPort(int timeout = 50)
    {
        if (isStreaming)
        {
            string message;
            ardunioPort.ReadTimeout = timeout;
            message = ardunioPort.ReadLine();
            if (message != null)
                return message;
            else
            {
                return "has timed out";
            }
        }
        else
        {
            return "port is not open";
        }
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
