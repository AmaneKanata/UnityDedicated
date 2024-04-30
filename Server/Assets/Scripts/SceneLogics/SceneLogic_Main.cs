using Framework.Network;
using Protocol;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using MEC;
using System.Collections.Generic;
using Unity.VisualScripting;

enum ItemState
{
    Idle,
    Spawned,
}

public class SceneLogic_Main : MonoBehaviour
{
    private int LoadCnt = 0;

    private float itemSpawnInterval = 5.0f;
    private ItemState itemState;
    private string occupier = "";
    private int winningScore = 3;

    private void Start()
    {
        S_LOAD_SCENE pkt = new S_LOAD_SCENE();
        NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));

        GPHManager.Instance.GPH.AddHandler(Handle_C_LOAD_SCENE_COMPLETE);

        SceneManager.Instance.Fade(false);
    }

    public void OnDestroy()
    {
        GPHManager.Instance.GPH.RemoveHandler(Handle_C_LOAD_SCENE_COMPLETE);
    }
    public void Handle_C_LOAD_SCENE_COMPLETE( C_LOAD_SCENE_COMPLETE pkt, Connection connection )
    {
        LoadCnt++;

        if (LoadCnt == NetworkManager.Instance.Clients.Count)
            StartGame();
    }

    private void StartGame()
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
            player.GetComponent<PlayerController>().OnItemOccupy += SetOccupier;

            S_INSTANTIATE pkt = new S_INSTANTIATE()
            {
                PlayerId = cnt++,
                Position = Converter.Convert(position),
                Rotation = rotation,
            };
            NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));
        }

        Timing.RunCoroutine(Game());
    }

    public IEnumerator<float> Game()
    {
        GameObject item = null;

        Dictionary<string, int> scores = new Dictionary<string, int>();
        foreach (string clientId in NetworkManager.Instance.Clients.Keys)
            scores.Add(clientId, 0);

        itemState = ItemState.Idle;

        bool finished = false;

        while (true)
        {
            switch(itemState)
            {
                case ItemState.Idle:
                    {
                        yield return Timing.WaitForSeconds(itemSpawnInterval);
                        itemState = ItemState.Spawned;

                        Vector3 itemPosition = new Vector3(0, 0, 1);
                        
                        item = Instantiate(Resources.Load<GameObject>("Prefabs/Item"), itemPosition, Quaternion.identity);

                        S_SPAWN_ITEM pkt = new S_SPAWN_ITEM()
                        {
                            Position = Converter.Convert(itemPosition),
                        };
                        NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));

                        break;
                    }
                case ItemState.Spawned:
                    while(occupier == "")
                        yield return Timing.WaitForOneFrame;

                    Destroy(item);

                    {
                        S_DESTROY_ITEM pkt = new S_DESTROY_ITEM();
                        NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));
                    }

                    scores[occupier] += 1;
                    if (scores[occupier] == winningScore)
                    {
                        S_FINISH_GAME pkt = new();
                        NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));

                        finished = true;
                    }

                    occupier = "";
                    itemState = ItemState.Idle;

                    break;
            }

            if (finished)
                break;
        }

        SceneManager.Instance.Fade(true);
        SceneManager.Instance.LoadScene(SceneName.Lobby);
    }

    public void SetOccupier(string id)
    {
        if(occupier == "")
            occupier = id;
    }
}