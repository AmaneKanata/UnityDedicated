using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Framework.Network
{
    public class Acceptor
    {
        private Socket listenSocket;
        private Queue<Socket> waitingSockets;
        private object lockObj = new object();

        public Acceptor( IPEndPoint endPoint )
        {
            waitingSockets = new Queue<Socket>();

            listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(endPoint);
            listenSocket.Listen(10);

            StartAccept();
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

        public Queue<Socket> GetWaitingSockets()
        {
            Queue<Socket> res;

            lock (lockObj)
            {
                res = waitingSockets;
                waitingSockets = new Queue<Socket>();
            }

            return res;
        }

        public void Close()
        {
            listenSocket.Close();
        }
    }
}