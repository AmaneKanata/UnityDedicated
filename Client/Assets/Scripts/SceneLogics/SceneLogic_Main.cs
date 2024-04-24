using Protocol;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic_Main : MonoBehaviour
{
    Dictionary<int, GameObject> Players;

    void Start()
    {
        Players = new Dictionary<int, GameObject>();
        
        NetworkManager.Instance.Client.packetHandler.AddHandler(Handle_S_INSTANTIATE);
    }

    public void Connect()
    {
        NetworkManager.Instance.Connect("TestClient");
    }

    public void Handle_S_INSTANTIATE( S_INSTANTIATE pkt )
    {
        var player = Instantiate(Resources.Load("Prefabs/Player"), Converter.Convert(pkt.Position), Converter.Convert(pkt.Rotation)) as GameObject;
        Players.Add(pkt.PlayerId, player);
    }
}
