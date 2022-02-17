using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class TCPServer : MonoBehaviour
{

    public string hostIp = "127.0.0.1";
    public Int32 port = 5066;

    [HideInInspector]
    public String data = null;

    private Thread t;

    // Start is called before the first frame update
    void Start()
    {
        t = new Thread(new ThreadStart(StartServer));
        t.IsBackground = true;
        t.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        // close the thread when the application quits
            t.Abort();
    }

    private void StartServer() {
        TcpListener server=null;
        try {
                IPAddress addr = IPAddress.Parse(hostIp);
                server = new TcpListener(addr, port);
                server.Start();
                Debug.Log("Server started at" + hostIp + ":" + port);

                Byte[] bytes = new byte[256];
                data = null;
            while(true)
            {
                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                Debug.Log("Waiting for a connection... ");
                TcpClient client = server.AcceptTcpClient();
                Debug.Log("Connected!");

                data = null;

                NetworkStream stream = client.GetStream();

                int i;  
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                }

                Debug.Log("Terminating connection");
                stream.Close();
                client.Close();
            }
        } catch(SocketException e) {
            Debug.Log("SocketException: {" + e + "}");
        }finally {
            Debug.Log("Stopping server");
            server.Stop();
        }
    }
}
