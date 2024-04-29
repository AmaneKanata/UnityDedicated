using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class GlobalPacketHandler
    {
        public Dictionary<ushort, Action<IMessage, Connection>> Handlers = new();
        private Action<Protocol.C_ENTER, Connection> C_ENTER_Handler;
        private Action<Protocol.C_LEAVE, Connection> C_LEAVE_Handler;
        private Action<Protocol.C_GET_CLIENT, Connection> C_GET_CLIENT_Handler;
        private Action<Protocol.C_PING, Connection> C_PING_Handler;
        private Action<Protocol.C_SERVERTIME, Connection> C_SERVERTIME_Handler;
        private Action<Protocol.C_TEST, Connection> C_TEST_Handler;
        private Action<Protocol.C_READY, Connection> C_READY_Handler;
        private Action<Protocol.C_LOAD_SCENE_COMPLETE, Connection> C_LOAD_SCENE_COMPLETE_Handler;
        private Action<Protocol.C_PLAYER_INPUT, Connection> C_PLAYER_INPUT_Handler;

        public GlobalPacketHandler()
        {
            Handlers.Add(0, _Handle_C_ENTER);
            Handlers.Add(2, _Handle_C_LEAVE);
            Handlers.Add(3, _Handle_C_GET_CLIENT);
            Handlers.Add(7, _Handle_C_PING);
            Handlers.Add(9, _Handle_C_SERVERTIME);
            Handlers.Add(11, _Handle_C_TEST);
            Handlers.Add(100, _Handle_C_READY);
            Handlers.Add(102, _Handle_C_LOAD_SCENE_COMPLETE);
            Handlers.Add(105, _Handle_C_PLAYER_INPUT);
        }
        public void AddHandler( Action<Protocol.C_ENTER, Connection> handler )
        {
            C_ENTER_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_ENTER, Connection> handler )
        {
            C_ENTER_Handler -= handler;
        }
        private void _Handle_C_ENTER( IMessage message, Connection connection )
        {
            C_ENTER_Handler?.Invoke((Protocol.C_ENTER)message, connection);
        }
        public void AddHandler( Action<Protocol.C_LEAVE, Connection> handler )
        {
            C_LEAVE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_LEAVE, Connection> handler )
        {
            C_LEAVE_Handler -= handler;
        }
        private void _Handle_C_LEAVE( IMessage message, Connection connection )
        {
            C_LEAVE_Handler?.Invoke((Protocol.C_LEAVE)message, connection);
        }
        public void AddHandler( Action<Protocol.C_GET_CLIENT, Connection> handler )
        {
            C_GET_CLIENT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_GET_CLIENT, Connection> handler )
        {
            C_GET_CLIENT_Handler -= handler;
        }
        private void _Handle_C_GET_CLIENT( IMessage message, Connection connection )
        {
            C_GET_CLIENT_Handler?.Invoke((Protocol.C_GET_CLIENT)message, connection);
        }
        public void AddHandler( Action<Protocol.C_PING, Connection> handler )
        {
            C_PING_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_PING, Connection> handler )
        {
            C_PING_Handler -= handler;
        }
        private void _Handle_C_PING( IMessage message, Connection connection )
        {
            C_PING_Handler?.Invoke((Protocol.C_PING)message, connection);
        }
        public void AddHandler( Action<Protocol.C_SERVERTIME, Connection> handler )
        {
            C_SERVERTIME_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_SERVERTIME, Connection> handler )
        {
            C_SERVERTIME_Handler -= handler;
        }
        private void _Handle_C_SERVERTIME( IMessage message, Connection connection )
        {
            C_SERVERTIME_Handler?.Invoke((Protocol.C_SERVERTIME)message, connection);
        }
        public void AddHandler( Action<Protocol.C_TEST, Connection> handler )
        {
            C_TEST_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_TEST, Connection> handler )
        {
            C_TEST_Handler -= handler;
        }
        private void _Handle_C_TEST( IMessage message, Connection connection )
        {
            C_TEST_Handler?.Invoke((Protocol.C_TEST)message, connection);
        }
        public void AddHandler( Action<Protocol.C_READY, Connection> handler )
        {
            C_READY_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_READY, Connection> handler )
        {
            C_READY_Handler -= handler;
        }
        private void _Handle_C_READY( IMessage message, Connection connection )
        {
            C_READY_Handler?.Invoke((Protocol.C_READY)message, connection);
        }
        public void AddHandler( Action<Protocol.C_LOAD_SCENE_COMPLETE, Connection> handler )
        {
            C_LOAD_SCENE_COMPLETE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_LOAD_SCENE_COMPLETE, Connection> handler )
        {
            C_LOAD_SCENE_COMPLETE_Handler -= handler;
        }
        private void _Handle_C_LOAD_SCENE_COMPLETE( IMessage message, Connection connection )
        {
            C_LOAD_SCENE_COMPLETE_Handler?.Invoke((Protocol.C_LOAD_SCENE_COMPLETE)message, connection);
        }
        public void AddHandler( Action<Protocol.C_PLAYER_INPUT, Connection> handler )
        {
            C_PLAYER_INPUT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_PLAYER_INPUT, Connection> handler )
        {
            C_PLAYER_INPUT_Handler -= handler;
        }
        private void _Handle_C_PLAYER_INPUT( IMessage message, Connection connection )
        {
            C_PLAYER_INPUT_Handler?.Invoke((Protocol.C_PLAYER_INPUT)message, connection);
        }
    }
}