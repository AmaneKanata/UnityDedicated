using Framework.Network;
using Protocol;
using UnityEngine;

public class SceneLogic_Lobby : MonoBehaviour
{
    public void Start()
    {
        NetworkManager.Instance.StartAccept();

        GPHManager.Instance.GPH.AddHandler(Handle_C_ENTER);
    }

    public void Handle_C_ENTER(C_ENTER pkt, Connection connection)
    {
        Debug.Log("Handle_C_ENTER");

        Client client = connection as Client;
        if (client == null)
        {
            Debug.LogError("Handle_C_ENTER: connection is not a Client");
            return;
        }

        Debug.Log(client.Id);
    }
}