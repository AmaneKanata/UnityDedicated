using Framework.Network;
using Protocol;
using UnityEngine;

public class SceneLogic_Lobby : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_LOAD_SCENE);

        UIManager.Instance.OpenPanel<LobbyPanel>();
    }

    public void Handle_S_LOAD_SCENE( S_LOAD_SCENE pkt )
    {
        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Main);
    }
}
