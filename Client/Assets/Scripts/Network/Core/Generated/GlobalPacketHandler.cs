using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class GlobalPacketHandler
    {
        public Dictionary<ushort, Action<IMessage, Connection>> Handlers = new();
        private Action<Protocol.S_ENTER, Connection> S_ENTER_Handler;
        private Action<Protocol.S_ADD_CLIENT, Connection> S_ADD_CLIENT_Handler;
        private Action<Protocol.S_REMOVE_CLIENT, Connection> S_REMOVE_CLIENT_Handler;
        private Action<Protocol.S_DISCONNECT, Connection> S_DISCONNECT_Handler;
        private Action<Protocol.S_PING, Connection> S_PING_Handler;
        private Action<Protocol.S_SERVERTIME, Connection> S_SERVERTIME_Handler;
        private Action<Protocol.S_TEST, Connection> S_TEST_Handler;
        private Action<Protocol.S_LOAD_SCENE, Connection> S_LOAD_SCENE_Handler;
        private Action<Protocol.S_START_GAME, Connection> S_START_GAME_Handler;
        private Action<Protocol.S_INSTANTIATE, Connection> S_INSTANTIATE_Handler;

        public GlobalPacketHandler()
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
            Handlers.Add(104, _Handle_S_INSTANTIATE);
        }
        public void AddHandler( Action<Protocol.S_ENTER, Connection> handler )
        {
            S_ENTER_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_ENTER, Connection> handler )
        {
            S_ENTER_Handler -= handler;
        }
        private void _Handle_S_ENTER( IMessage message, Connection connection )
        {
            S_ENTER_Handler?.Invoke((Protocol.S_ENTER)message, connection);
        }
        public void AddHandler( Action<Protocol.S_ADD_CLIENT, Connection> handler )
        {
            S_ADD_CLIENT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_ADD_CLIENT, Connection> handler )
        {
            S_ADD_CLIENT_Handler -= handler;
        }
        private void _Handle_S_ADD_CLIENT( IMessage message, Connection connection )
        {
            S_ADD_CLIENT_Handler?.Invoke((Protocol.S_ADD_CLIENT)message, connection);
        }
        public void AddHandler( Action<Protocol.S_REMOVE_CLIENT, Connection> handler )
        {
            S_REMOVE_CLIENT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_REMOVE_CLIENT, Connection> handler )
        {
            S_REMOVE_CLIENT_Handler -= handler;
        }
        private void _Handle_S_REMOVE_CLIENT( IMessage message, Connection connection )
        {
            S_REMOVE_CLIENT_Handler?.Invoke((Protocol.S_REMOVE_CLIENT)message, connection);
        }
        public void AddHandler( Action<Protocol.S_DISCONNECT, Connection> handler )
        {
            S_DISCONNECT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_DISCONNECT, Connection> handler )
        {
            S_DISCONNECT_Handler -= handler;
        }
        private void _Handle_S_DISCONNECT( IMessage message, Connection connection )
        {
            S_DISCONNECT_Handler?.Invoke((Protocol.S_DISCONNECT)message, connection);
        }
        public void AddHandler( Action<Protocol.S_PING, Connection> handler )
        {
            S_PING_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_PING, Connection> handler )
        {
            S_PING_Handler -= handler;
        }
        private void _Handle_S_PING( IMessage message, Connection connection )
        {
            S_PING_Handler?.Invoke((Protocol.S_PING)message, connection);
        }
        public void AddHandler( Action<Protocol.S_SERVERTIME, Connection> handler )
        {
            S_SERVERTIME_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_SERVERTIME, Connection> handler )
        {
            S_SERVERTIME_Handler -= handler;
        }
        private void _Handle_S_SERVERTIME( IMessage message, Connection connection )
        {
            S_SERVERTIME_Handler?.Invoke((Protocol.S_SERVERTIME)message, connection);
        }
        public void AddHandler( Action<Protocol.S_TEST, Connection> handler )
        {
            S_TEST_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_TEST, Connection> handler )
        {
            S_TEST_Handler -= handler;
        }
        private void _Handle_S_TEST( IMessage message, Connection connection )
        {
            S_TEST_Handler?.Invoke((Protocol.S_TEST)message, connection);
        }
        public void AddHandler( Action<Protocol.S_LOAD_SCENE, Connection> handler )
        {
            S_LOAD_SCENE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_LOAD_SCENE, Connection> handler )
        {
            S_LOAD_SCENE_Handler -= handler;
        }
        private void _Handle_S_LOAD_SCENE( IMessage message, Connection connection )
        {
            S_LOAD_SCENE_Handler?.Invoke((Protocol.S_LOAD_SCENE)message, connection);
        }
        public void AddHandler( Action<Protocol.S_START_GAME, Connection> handler )
        {
            S_START_GAME_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_START_GAME, Connection> handler )
        {
            S_START_GAME_Handler -= handler;
        }
        private void _Handle_S_START_GAME( IMessage message, Connection connection )
        {
            S_START_GAME_Handler?.Invoke((Protocol.S_START_GAME)message, connection);
        }
        public void AddHandler( Action<Protocol.S_INSTANTIATE, Connection> handler )
        {
            S_INSTANTIATE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.S_INSTANTIATE, Connection> handler )
        {
            S_INSTANTIATE_Handler -= handler;
        }
        private void _Handle_S_INSTANTIATE( IMessage message, Connection connection )
        {
            S_INSTANTIATE_Handler?.Invoke((Protocol.S_INSTANTIATE)message, connection);
        }
    }
}