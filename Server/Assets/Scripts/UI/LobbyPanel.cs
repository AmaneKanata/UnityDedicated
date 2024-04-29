using Framework.Network;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : PanelBase
{
    VerticalLayoutGroup list;

    Dictionary<string, ClientReadyState> clientReadyStates;

    private void Awake()
    {
        list = transform.Find("List").GetComponent<VerticalLayoutGroup>();

        clientReadyStates = new Dictionary<string, ClientReadyState>();
    }

    private void Start()
    {
        NetworkManager.Instance.OnEnterSucceeded += OnEnterSucceeded;
        GPHManager.Instance.GPH.AddHandler(Handle_C_READY);
    }

    private void OnDestroy()
    {
        NetworkManager.Instance.OnEnterSucceeded -= OnEnterSucceeded;
        GPHManager.Instance.GPH.RemoveHandler(Handle_C_READY);
    }

    public void OnEnterSucceeded( Connection connection )
    {
        var client = connection as Client;

        var clientLobbyState = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ClientReadyState"));
        clientLobbyState.transform.SetParent(list.transform);

        clientLobbyState.GetComponent<ClientReadyState>().SetID(client.Id);
        clientLobbyState.GetComponent<ClientReadyState>().SetReady(false);

        clientReadyStates.Add(client.Id, clientLobbyState.GetComponent<ClientReadyState>());
    }

    public void Handle_C_READY( Protocol.C_READY pkt, Connection connection )
    {
        Client client = connection as Client;

        clientReadyStates[client.Id].SetReady(pkt.IsReady);
    }
}