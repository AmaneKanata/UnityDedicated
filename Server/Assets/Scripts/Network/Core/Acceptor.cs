using MEC;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Framework.Network
{
    public class Acceptor : MonoBehaviour
    {
        private Socket listenSocket;
        private Queue<Socket> waitingSockets;
        object lockObj = new object();

        public void Start()
        {
            waitingSockets = new Queue<Socket>();

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.8"), 7777);

            listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(endPoint);
            listenSocket.Listen(10);

            StartAccept();

            Timing.RunCoroutine(ProcessSocket());
        }

        public void StartAccept()
        {
            try
            {
                listenSocket.BeginAccept(new AsyncCallback(ProcessAccept), listenSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
        }

        public void ProcessAccept( IAsyncResult ar )
        {
            try
            {
                Socket clientSocket = listenSocket.EndAccept(ar);

                lock (lockObj)
                {
                    waitingSockets.Enqueue(clientSocket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }
            finally
            {
                StartAccept();
            }
        }

        public IEnumerator<float> ProcessSocket()
        {
            while (true)
            {
                int currentCount;
                lock (lockObj)
                {
                    currentCount = waitingSockets.Count;
                }

                for (int i = 0; i < currentCount; i++)
                {
                    Socket clientSocket;
                    lock (lockObj)
                    {
                        clientSocket = waitingSockets.Dequeue();
                    }

                    clientSocket.NoDelay = true;

                    Connection connection = new();
                    connection.SetSession(clientSocket);
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        public void Close()
        {
            listenSocket.Close();
        }
    }
}