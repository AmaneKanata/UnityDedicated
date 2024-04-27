using Framework.Network;
using Protocol;
using UnityEngine;

public class SceneLogic_Lobby : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_LOAD_SCENE);
    }

    public void Connect()
    {
        NetworkManager.Instance.Connect("TestClient");
    }

    public void Ready()
    {
        C_READY ready = new()
        {
            IsReady = true
        };
        NetworkManager.Instance.Client.Send(PacketManager.MakeSendBuffer(ready));
    }

    public void Handle_S_LOAD_SCENE( S_LOAD_SCENE pkt )
    {
        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Main);
    }
}
