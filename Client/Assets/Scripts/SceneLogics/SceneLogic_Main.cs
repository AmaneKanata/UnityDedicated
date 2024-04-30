using Framework.Network;
using Protocol;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic_Main : MonoBehaviour
{
    Dictionary<int, GameObject> Players;
    GameObject item = null;

    void Start()
    {
        Players = new Dictionary<int, GameObject>();

        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_START_GAME);
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_INSTANTIATE);
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_SPAWN_ITEM);
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_DESTROY_ITEM);
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_FINISH_GAME);

        SceneManager.Instance.Fade(false);

        C_LOAD_SCENE_COMPLETE res = new();
        NetworkManager.Instance.Client.Send(PacketManager.MakeSendBuffer(res));
    }

    private void OnDestroy()
    {
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_START_GAME);
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_INSTANTIATE);
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_SPAWN_ITEM);
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_DESTROY_ITEM);
        NetworkManager.Instance.Client.packetHandler.RemoveHandler(Handle_S_FINISH_GAME);
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

    public void Handle_S_SPAWN_ITEM( S_SPAWN_ITEM pkt )
    {
        item = Instantiate(Resources.Load("Prefabs/Item"), Converter.Convert(pkt.Position), Quaternion.identity) as GameObject;
    }

    public void Handle_S_DESTROY_ITEM( S_DESTROY_ITEM pkt )
    {
        Destroy(item);
    }

    public void Handle_S_FINISH_GAME( S_FINISH_GAME pkt )
    {
        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Lobby);
    }
}
