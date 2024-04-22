using Protocol;
using UnityEngine;

namespace Framework.Network
{
    public class Client : Connection
    {
        public string Id { get; private set; }

        public Client()
        {
            packetHandler.AddHandler(Handle_C_ENTER);
        }

        public void Handle_C_ENTER(C_ENTER pkt)
        {
            Debug.Log("Handle_C_ENTER");

            Id = pkt.ClientId;
            NetworkManager.Instance.AddClient(this);
        }
    }
}
