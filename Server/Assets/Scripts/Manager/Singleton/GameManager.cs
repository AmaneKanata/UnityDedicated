using Framework.Network;
using Protocol;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public enum GameState
{
    Idle,
    Running,
}

public class GameManager : SingletonManager<GameManager>
{
    public GameState GameState { get; private set; } = GameState.Idle;

    private int readyNumber = 0;
    private int loadSceneCompleteNumber = 0;

    public void Ready( bool isReady )
    {
        readyNumber += isReady ? 1 : -1;

        if (readyNumber == NetworkManager.Instance.Clients.Count)
            LoadScene();
    }

    public void LoadScene()
    {
        GameState = GameState.Running;
        //Load Map

        S_LOAD_SCENE pkt = new S_LOAD_SCENE();
        NetworkManager.Instance.BroadCast(PacketManager.MakeSendBuffer(pkt));
    }

    public void LoadSceneComplete()
    {
        loadSceneCompleteNumber++;

        if (loadSceneCompleteNumber == NetworkManager.Instance.Clients.Count)
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
