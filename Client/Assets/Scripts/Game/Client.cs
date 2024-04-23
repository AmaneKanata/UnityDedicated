using Protocol;
using UnityEngine;

namespace Framework.Network
{
    public class Client : Connection
    {
        public string Id { get; set; }

        public Client()
        {
            packetHandler.AddHandler(Handle_S_ENTER);
            packetHandler.AddHandler(Handle_S_LOAD_SCENE);
            packetHandler.AddHandler(Handle_S_START_GAME);
        }

        public void Handle_S_ENTER( S_ENTER pkt )
        {
            Debug.Log("Handle_C_ENTER");

            if(pkt.Result != "SUCCESS")
            {
                Close();
                return;
            }

            C_READY ready = new()
            {
                IsReady = true
            };
            Send(PacketManager.MakeSendBuffer(ready));
        }

        public void Handle_S_LOAD_SCENE( S_LOAD_SCENE pkt )
        {
            C_LOAD_SCENE_COMPLETE res = new();
            Send(PacketManager.MakeSendBuffer(res));
        }

        public void Handle_S_START_GAME( S_START_GAME pkt )
        {
            Debug.Log("Handle_S_START_GAME");
        }
    }
}
