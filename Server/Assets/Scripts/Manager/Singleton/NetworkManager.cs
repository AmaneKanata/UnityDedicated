using Framework.Network;
using MEC;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetworkManager : SingletonManager<NetworkManager>
{
    private string ip = "192.168.0.8";
    private int port = 7777;
    private Acceptor acceptor;

    public List<Connection> Connections { get; private set; }

    public void Awake()
    {
        Connections = new List<Connection>();
    }

    public void StartAccept()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        acceptor = new Acceptor(endPoint);
        acceptor.StartAccept();
        Debug.Log("StartAccept");

        Timing.RunCoroutine(ProcessSocket());
    }

    public IEnumerator<float> ProcessSocket()
    {
        while (true)
        {
            var waitingSockets = acceptor.GetWaitingSockets();

            while (waitingSockets.Count > 0)
            {
                Socket clientSocket = waitingSockets.Dequeue();

                clientSocket.NoDelay = true;

                Connection connection = new Connection();
                connection.SetSession(clientSocket);

                Connections.Add(connection);
            }

            yield return Timing.WaitForOneFrame;
        }
    }
}
