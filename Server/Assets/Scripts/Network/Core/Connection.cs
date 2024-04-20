using Google.Protobuf;
using MEC;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Framework.Network
{
    public enum ConnectionState
    {
        Connected,
        Closed
    }

    public class Connection
    {
        public string ConnectionId { get; set; }

        protected ClientSession session;

        public ClientSession Session
        {
            get => session;
            private set
            {
                session = value;
                session.disconnectedHandler += _OnDisconnected;
                session.receivedHandler += _OnRecv;
            }
        }

        public PacketQueue PacketQueue { get; }
        public PacketHandler packetHandler { get; }

        protected ConnectionState state;
        public ConnectionState State => state;

        public Action connectedHandler;
        public Action disconnectedHandler;

        CoroutineHandle packetUpdate;

        public Connection()
        {
            state = ConnectionState.Closed;

            packetHandler = new PacketHandler();
            packetHandler.AddHandler(Handle_C_ENTER);

            PacketQueue = new();

            packetUpdate = Timing.RunCoroutine(PacketUpdate());
        }

        ~Connection()
        {
            UnityEngine.Debug.Log("Connection Destructor");

            packetHandler.RemoveHandler(Handle_C_ENTER);

            Timing.KillCoroutines(packetUpdate);
        }

        public void SetSession( Socket socket )
        {
            ClientSession session = new();
            Session = session;
            Session.Start(socket);

            _OnConnected();
        }

        private void _OnConnected()
        {
            state = ConnectionState.Connected;

            connectedHandler?.Invoke();
        }

        private void _OnDisconnected()
        {
            state = ConnectionState.Closed;

            disconnectedHandler?.Invoke();
        }

        protected void _OnRecv( ArraySegment<byte> buffer )
        {
            PacketManager.OnRecv(buffer, this);
        }

        public void Send( ArraySegment<byte> pkt )
        {
            if (state == ConnectionState.Connected)
                Session.Send(pkt);
        }

        private void Handle_C_ENTER( Protocol.C_ENTER pkt )
        {
            Debug.Log($"Handle_C_ENTER: {pkt.ClientId}");
        }

        public void Handle_C_PING( Protocol.C_PING pkt )
        {
            Debug.Log($"Handle_C_PING");
        }

        public void Close()
        {
            if (state == ConnectionState.Closed)
            {
                return;
            }

            state = ConnectionState.Closed;

            session?.RegisterDisconnect();
        }

        private IEnumerator<float> PacketUpdate()
        {
            while (true)
            {
                if (state == ConnectionState.Connected || !PacketQueue.Empty())
                {
                    System.Collections.Generic.List<PacketMessage> packets = PacketQueue.PopAll();

                    for (int i = 0; i < packets.Count; i++)
                    {
                        PacketMessage packet = packets[i];

                        packetHandler.Handlers.TryGetValue(packet.Id, out Action<IMessage> handler);

                        handler?.Invoke(packet.Message);
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
