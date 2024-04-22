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

    private List<Client> tempClients;
    public Dictionary<string, Client> Clients { get; private set; }

    public void Awake()
    {
        tempClients = new List<Client>();
        Clients = new Dictionary<string, Client>();
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

                Client client = new Client();
                client.SetSession(clientSocket);

                tempClients.Add(client);
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    public void AddClient( Client client )
    {
        tempClients.Remove(client);

        if (Clients.ContainsKey(client.Id))
            Clients.Remove(client.Id);

        Clients.Add(client.Id, client);
    }
}
