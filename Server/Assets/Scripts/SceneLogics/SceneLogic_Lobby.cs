using Framework.Network;
using Protocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogic_Lobby : MonoBehaviour
{
    private VerticalLayoutGroup clientLobbyStates;

    public void Start()
    {
        clientLobbyStates = GameObject.Find("ClientLobbyStates").GetComponent<VerticalLayoutGroup>();

        NetworkManager.Instance.StartAccept();

        NetworkManager.Instance.OnEnterSucceeded += OnEnterSucceeded;
        
        GPHManager.Instance.GPH.AddHandler(Handle_C_READY);
    }

    public void OnEnterSucceeded( Connection connection )
    {
        var clientLobbyState = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ClientLobbyState"));
        var client = connection as Client;
        clientLobbyState.GetComponentInChildren<TMP_Text>().text = client.Id;
        clientLobbyState.transform.SetParent(clientLobbyStates.transform);
    }

    public void Handle_C_READY( Protocol.C_READY pkt, Connection connection )
    {
        Client client = connection as Client;
    }
}