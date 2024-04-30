using Framework.Network;
using Protocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLogic_Lobby : MonoBehaviour
{
    private int readyCnt = 0;

    private void Start()
    {
        NetworkManager.Instance.StartAccept();

        GPHManager.Instance.GPH.AddHandler(Handle_C_Ready);
    }

    private void OnDestroy()
    {
        GPHManager.Instance.GPH.RemoveHandler(Handle_C_Ready);
    }

    public void Handle_C_Ready( C_READY pkt, Connection connection )
    {
        readyCnt += pkt.IsReady ? 1 : -1;

        if (readyCnt == NetworkManager.Instance.Clients.Count)
            LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Main);
    }
}