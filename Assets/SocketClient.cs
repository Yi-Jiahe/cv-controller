using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
    private static Socket socket;

    // Start is called before the first frame update
    void Start()
    {
        string host = "localhost";
        int port = 12345;

        ConnectSocket(host, port);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static FaceData GetData()
    {
        Debug.Log("Time start: " + Time.time);

        string request = "Hit me";
        Byte[] requestBytes = Encoding.UTF8.GetBytes(request);
        Byte[] responseBytes = new Byte[1024];

        if (socket == null) {
            return null;
        }

        try
        {
            Debug.Log("Requesting for data");
            socket.Send(requestBytes, requestBytes.Length, 0);

            int tries = 0;
            int bytes = 0;
            string responseString = "";
            /*
            do {
                tries += 1;
                bytes = socket.Receive(responseBytes, responseBytes.Length, 0);
                responseString = responseString + Encoding.UTF8.GetString(responseBytes, 0, bytes);
                Debug.Log(responseString);
                Debug.Log(bytes);
            } while (bytes > 0 || tries < 3);
            */

            bytes = socket.Receive(responseBytes, responseBytes.Length, 0);
            responseString = responseString + Encoding.UTF8.GetString(responseBytes, 0, bytes);
            Debug.Log(bytes);
            Debug.Log(responseString);

            Debug.Log("Time end: " + Time.time);

            return JsonUtility.FromJson<FaceData>(responseString);
        } catch (Exception e) {
            Debug.Log("Socket error: " + e);
        }

        return null;        
    }

    private static void ConnectSocket(string server, int port)
    {
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());

        foreach(IPAddress address in ipHost.AddressList)
        {
            IPEndPoint localEndPoint = new IPEndPoint(address, 12345);

            Socket tempSocket =
                new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try {
                Debug.Log("Attempting to connect to " + localEndPoint.ToString());
                tempSocket.Connect(localEndPoint);

                if(tempSocket.Connected)
                {
                    Debug.Log("Successfully connected to " + localEndPoint.ToString());
                    socket = tempSocket;
                    return;
                }
                else
                {
                    continue;
                }
            }
            catch (Exception e) {
                Debug.Log("Socket error: " + e);
            }
        }
    }
}
