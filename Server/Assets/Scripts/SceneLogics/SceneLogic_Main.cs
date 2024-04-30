using Framework.Network;
using Protocol;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SceneLogic_Main : MonoBehaviour
{
    private int LoadCnt = 0;

    public void Start()
    {
        S_LOAD_SCENE pkt = new S_LOAD_SCENE();
        NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));

        GPHManager.Instance.GPH.AddHandler(Handle_C_LOAD_SCENE_COMPLETE);

        SceneManager.Instance.Fade(false);
    }

    public void Handle_C_LOAD_SCENE_COMPLETE( C_LOAD_SCENE_COMPLETE pkt, Connection connection )
    {
        LoadCnt++;

        if (LoadCnt == NetworkManager.Instance.Clients.Count)
            StartGame();
    }

    public void StartGame()
    {
        {
            S_START_GAME pkt = new S_START_GAME();
            NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));
        }

        int cnt = 0;
        foreach (Client client in NetworkManager.Instance.Clients.Values)
        {
            Vector3 position = new Vector3(0, 0, 1);
            float rotation = 0;

            var player = Instantiate(Resources.Load<GameObject>("Prefabs/Player"), position, Converter.Convert(rotation));

            player.GetComponent<PlayerController>().SetOwner(client);

            S_INSTANTIATE pkt = new S_INSTANTIATE()
            {
                PlayerId = cnt++,
                Position = Converter.Convert(position),
                Rotation = rotation,
            };
            NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));
        }
    }
}