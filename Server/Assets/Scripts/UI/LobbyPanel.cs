using Framework.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : PanelBase
{
    VerticalLayoutGroup list;

    private void Awake()
    {
        list = transform.Find("List").GetComponent<VerticalLayoutGroup>();
    }

    private void Start()
    {
        NetworkManager.Instance.OnEnterSucceeded += OnEnterSucceeded;
        GPHManager.Instance.GPH.AddHandler(Handle_C_READY);
    }

    public void OnEnterSucceeded( Connection connection )
    {
        var clientLobbyState = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ClientReadyState"));
        var client = connection as Client;
        clientLobbyState.GetComponentInChildren<TMP_Text>().text = client.Id;
        clientLobbyState.transform.SetParent(list.transform);
    }

    public void Handle_C_READY( Protocol.C_READY pkt, Connection connection )
    {
        Client client = connection as Client;
    }

    override public void OnOpen() { }

    override public void OnClose() { }
}