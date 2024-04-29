using Protocol;
using UnityEngine;

namespace Framework.Network
{
    public class Client : Connection
    {
        public string Id { get; set; }

        public Client()
        {
            packetHandler.AddHandler(Handle_C_READY);
            packetHandler.AddHandler(Handle_C_LOAD_SCENE_COMPLETE);
        }

        public void Handle_C_READY(C_READY pkt)
        {
            GameManager.Instance.Ready(pkt.IsReady);
        }

        public void Handle_C_LOAD_SCENE_COMPLETE(C_LOAD_SCENE_COMPLETE pkt)
        {
            GameManager.Instance.LoadSceneComplete();
        }
    }
}
