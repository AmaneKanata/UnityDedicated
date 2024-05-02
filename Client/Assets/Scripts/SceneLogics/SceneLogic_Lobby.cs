using Protocol;
using UnityEngine;

public class SceneLogic_Lobby : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.Client.disconnectedHandler += OnDisconnected;

        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_LOAD_SCENE);

        UIManager.Instance.OpenPanel<LobbyPanel>();

        SceneManager.Instance.Fade(false);
    }

    void OnDestroy()
    {
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_LOAD_SCENE);
    }

    public void Handle_S_LOAD_SCENE( S_LOAD_SCENE pkt )
    {
        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Main);
    }

    public void OnDisconnected()
    {
        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Lobby);
    }
}
