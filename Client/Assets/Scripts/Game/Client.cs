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
        }

        ~Client()
        {
            packetHandler.RemoveHandler(Handle_S_ENTER);
        }
        public void Handle_S_ENTER( S_ENTER pkt )
        {
            if (pkt.Result != "SUCCESS")
            {
                Close();
                return;
            }
        }
    }
}
