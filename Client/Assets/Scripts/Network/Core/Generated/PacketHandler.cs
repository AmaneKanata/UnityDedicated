using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class PacketHandler
    {
        public Dictionary<ushort, Action<IMessage>> Handlers = new();
        private Action<Protocol.S_ENTER> S_ENTER_Handler;
        private Action<Protocol.S_ADD_CLIENT> S_ADD_CLIENT_Handler;
        private Action<Protocol.S_REMOVE_CLIENT> S_REMOVE_CLIENT_Handler;
        private Action<Protocol.S_DISCONNECT> S_DISCONNECT_Handler;
        private Action<Protocol.S_PING> S_PING_Handler;
        private Action<Protocol.S_SERVERTIME> S_SERVERTIME_Handler;
        private Action<Protocol.S_TEST> S_TEST_Handler;
        private Action<Protocol.S_LOAD_SCENE> S_LOAD_SCENE_Handler;
        private Action<Protocol.S_START_GAME> S_START_GAME_Handler;

        public PacketHandler()
        {
            Handlers.Add(1, _Handle_S_ENTER);
            Handlers.Add(4, _Handle_S_ADD_CLIENT);
            Handlers.Add(5, _Handle_S_REMOVE_CLIENT);
            Handlers.Add(6, _Handle_S_DISCONNECT);
            Handlers.Add(8, _Handle_S_PING);
            Handlers.Add(10, _Handle_S_SERVERTIME);
            Handlers.Add(12, _Handle_S_TEST);
            Handlers.Add(101, _Handle_S_LOAD_SCENE);
            Handlers.Add(103, _Handle_S_START_GAME);
        }
        public void AddHandler( Action<Protocol.S_ENTER> handler )
        {
            S_ENTER_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_ENTER> handler )
        {
            S_ENTER_Handler -= handler;
        }
        private void _Handle_S_ENTER( IMessage message )
        {
            S_ENTER_Handler?.Invoke((Protocol.S_ENTER)message);
        }
        public void AddHandler( Action<Protocol.S_ADD_CLIENT> handler )
        {
            S_ADD_CLIENT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_ADD_CLIENT> handler )
        {
            S_ADD_CLIENT_Handler -= handler;
        }
        private void _Handle_S_ADD_CLIENT( IMessage message )
        {
            S_ADD_CLIENT_Handler?.Invoke((Protocol.S_ADD_CLIENT)message);
        }
        public void AddHandler( Action<Protocol.S_REMOVE_CLIENT> handler )
        {
            S_REMOVE_CLIENT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_REMOVE_CLIENT> handler )
        {
            S_REMOVE_CLIENT_Handler -= handler;
        }
        private void _Handle_S_REMOVE_CLIENT( IMessage message )
        {
            S_REMOVE_CLIENT_Handler?.Invoke((Protocol.S_REMOVE_CLIENT)message);
        }
        public void AddHandler( Action<Protocol.S_DISCONNECT> handler )
        {
            S_DISCONNECT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_DISCONNECT> handler )
        {
            S_DISCONNECT_Handler -= handler;
        }
        private void _Handle_S_DISCONNECT( IMessage message )
        {
            S_DISCONNECT_Handler?.Invoke((Protocol.S_DISCONNECT)message);
        }
        public void AddHandler( Action<Protocol.S_PING> handler )
        {
            S_PING_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_PING> handler )
        {
            S_PING_Handler -= handler;
        }
        private void _Handle_S_PING( IMessage message )
        {
            S_PING_Handler?.Invoke((Protocol.S_PING)message);
        }
        public void AddHandler( Action<Protocol.S_SERVERTIME> handler )
        {
            S_SERVERTIME_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_SERVERTIME> handler )
        {
            S_SERVERTIME_Handler -= handler;
        }
        private void _Handle_S_SERVERTIME( IMessage message )
        {
            S_SERVERTIME_Handler?.Invoke((Protocol.S_SERVERTIME)message);
        }
        public void AddHandler( Action<Protocol.S_TEST> handler )
        {
            S_TEST_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_TEST> handler )
        {
            S_TEST_Handler -= handler;
        }
        private void _Handle_S_TEST( IMessage message )
        {
            S_TEST_Handler?.Invoke((Protocol.S_TEST)message);
        }
        public void AddHandler( Action<Protocol.S_LOAD_SCENE> handler )
        {
            S_LOAD_SCENE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_LOAD_SCENE> handler )
        {
            S_LOAD_SCENE_Handler -= handler;
        }
        private void _Handle_S_LOAD_SCENE( IMessage message )
        {
            S_LOAD_SCENE_Handler?.Invoke((Protocol.S_LOAD_SCENE)message);
        }
        public void AddHandler( Action<Protocol.S_START_GAME> handler )
        {
            S_START_GAME_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_START_GAME> handler )
        {
            S_START_GAME_Handler -= handler;
        }
        private void _Handle_S_START_GAME( IMessage message )
        {
            S_START_GAME_Handler?.Invoke((Protocol.S_START_GAME)message);
        }
    }
}