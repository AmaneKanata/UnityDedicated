using Framework.Network;
using Protocol;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic_Main : MonoBehaviour
{
    Dictionary<int, GameObject> Players;

    void Start()
    {
        Players = new Dictionary<int, GameObject>();

        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_START_GAME);
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_INSTANTIATE);

        SceneManager.Instance.Fade(false);

        C_LOAD_SCENE_COMPLETE res = new();
        NetworkManager.Instance.Client.Send(PacketManager.MakeSendBuffer(res));
    }

    private void OnDestroy()
    {
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_START_GAME);
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_INSTANTIATE);
    }

    public void Handle_S_START_GAME( S_START_GAME pkt )
    {
        Debug.Log("Handle_S_START_GAME");
    }

    public void Handle_S_INSTANTIATE( S_INSTANTIATE pkt )
    {
        try
        {
            var player = Instantiate(Resources.Load("Prefabs/Player"), Converter.Convert(pkt.Position), Converter.Convert(pkt.Rotation)) as GameObject;
            Players.Add(pkt.PlayerId, player);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
