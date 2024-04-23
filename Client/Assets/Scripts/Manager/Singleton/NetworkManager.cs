using Framework.Network;
using Protocol;
using System.Net;
using UnityEngine;

public class NetworkManager : SingletonManager<NetworkManager>
{
    public Client Client { get; private set; }

    private readonly bool isLocal = true;

    private readonly string localAddress = "127.0.0.1";
    private readonly int localPort = 7777;


    private void Awake()
    {
        Client = new();
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    public IPEndPoint GetAddress()
    {
        if (isLocal)
        {
            return new(IPAddress.Parse(localAddress), localPort);
        }
        else
        {
            return null;
        }
    }

    public async void Connect( string connectionId )
    {
        if (Client.State == ConnectionState.Connected)
            return;

        IPEndPoint endPoint = GetAddress();
        if (endPoint == null)
        {
            Debug.Log("GetAddress Fail!");
            return;
        }

        bool success = await Connector.Connect(endPoint, Client);
        if (success)
        {
            Client.Id = connectionId;

            C_ENTER enter = new()
            {
                ClientId = connectionId
            };

            Client.Send(PacketManager.MakeSendBuffer(enter));
        }
    }

    public void Disconnect()
    {
        if (Client == null || Client.State == ConnectionState.Closed)
            return;

        Client.Send(PacketManager.MakeSendBuffer(new C_LEAVE()));
    }
}