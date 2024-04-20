using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;

namespace Framework.Network
{
    public class Acceptor
    {
        private Socket listenSocket;

        public Acceptor( IPEndPoint endPoint )
        {
            listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Listen(10);
            StartAccept();
        }

        public async void StartAccept()
        {
            try
            {
                Socket clientSocket = await listenSocket.AcceptAsync();

                clientSocket.NoDelay = true;

                Connection connection = new();
                connection.SetSession(clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
            }

            StartAccept();
        }
    }
}