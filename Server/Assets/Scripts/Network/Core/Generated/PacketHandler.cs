using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class PacketHandler
    {
        public Dictionary<ushort, Action<IMessage>> Handlers = new();
        private Action<Protocol.C_ENTER> C_ENTER_Handler;
        private Action<Protocol.C_LEAVE> C_LEAVE_Handler;
        private Action<Protocol.C_GET_CLIENT> C_GET_CLIENT_Handler;
        private Action<Protocol.C_PING> C_PING_Handler;
        private Action<Protocol.C_SERVERTIME> C_SERVERTIME_Handler;
        private Action<Protocol.C_TEST> C_TEST_Handler;
        private Action<Protocol.C_READY> C_READY_Handler;
        private Action<Protocol.C_LOAD_SCENE_COMPLETE> C_LOAD_SCENE_COMPLETE_Handler;
        private Action<Protocol.C_PLAYER_INPUT> C_PLAYER_INPUT_Handler;

        public PacketHandler()
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
        public void AddHandler( Action<Protocol.C_ENTER> handler )
        {
            C_ENTER_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_ENTER> handler )
        {
            C_ENTER_Handler -= handler;
        }
        private void _Handle_C_ENTER( IMessage message )
        {
            C_ENTER_Handler?.Invoke((Protocol.C_ENTER)message);
        }
        public void AddHandler( Action<Protocol.C_LEAVE> handler )
        {
            C_LEAVE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_LEAVE> handler )
        {
            C_LEAVE_Handler -= handler;
        }
        private void _Handle_C_LEAVE( IMessage message )
        {
            C_LEAVE_Handler?.Invoke((Protocol.C_LEAVE)message);
        }
        public void AddHandler( Action<Protocol.C_GET_CLIENT> handler )
        {
            C_GET_CLIENT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_GET_CLIENT> handler )
        {
            C_GET_CLIENT_Handler -= handler;
        }
        private void _Handle_C_GET_CLIENT( IMessage message )
        {
            C_GET_CLIENT_Handler?.Invoke((Protocol.C_GET_CLIENT)message);
        }
        public void AddHandler( Action<Protocol.C_PING> handler )
        {
            C_PING_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_PING> handler )
        {
            C_PING_Handler -= handler;
        }
        private void _Handle_C_PING( IMessage message )
        {
            C_PING_Handler?.Invoke((Protocol.C_PING)message);
        }
        public void AddHandler( Action<Protocol.C_SERVERTIME> handler )
        {
            C_SERVERTIME_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_SERVERTIME> handler )
        {
            C_SERVERTIME_Handler -= handler;
        }
        private void _Handle_C_SERVERTIME( IMessage message )
        {
            C_SERVERTIME_Handler?.Invoke((Protocol.C_SERVERTIME)message);
        }
        public void AddHandler( Action<Protocol.C_TEST> handler )
        {
            C_TEST_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_TEST> handler )
        {
            C_TEST_Handler -= handler;
        }
        private void _Handle_C_TEST( IMessage message )
        {
            C_TEST_Handler?.Invoke((Protocol.C_TEST)message);
        }
        public void AddHandler( Action<Protocol.C_READY> handler )
        {
            C_READY_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_READY> handler )
        {
            C_READY_Handler -= handler;
        }
        private void _Handle_C_READY( IMessage message )
        {
            C_READY_Handler?.Invoke((Protocol.C_READY)message);
        }
        public void AddHandler( Action<Protocol.C_LOAD_SCENE_COMPLETE> handler )
        {
            C_LOAD_SCENE_COMPLETE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_LOAD_SCENE_COMPLETE> handler )
        {
            C_LOAD_SCENE_COMPLETE_Handler -= handler;
        }
        private void _Handle_C_LOAD_SCENE_COMPLETE( IMessage message )
        {
            C_LOAD_SCENE_COMPLETE_Handler?.Invoke((Protocol.C_LOAD_SCENE_COMPLETE)message);
        }
        public void AddHandler( Action<Protocol.C_PLAYER_INPUT> handler )
        {
            C_PLAYER_INPUT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_PLAYER_INPUT> handler )
        {
            C_PLAYER_INPUT_Handler -= handler;
        }
        private void _Handle_C_PLAYER_INPUT( IMessage message )
        {
            C_PLAYER_INPUT_Handler?.Invoke((Protocol.C_PLAYER_INPUT)message);
        }
    }
}