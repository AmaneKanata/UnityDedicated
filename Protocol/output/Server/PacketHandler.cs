using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class PacketHandler
    {
        public Dictionary<ushort, Action<IMessage>> Handlers = new();
        private Action<Protocol.C_ENTER> C_ENTER_Handler;
        private Action<Protocol.C_REENTER> C_REENTER_Handler;
        private Action<Protocol.C_LEAVE> C_LEAVE_Handler;
        private Action<Protocol.C_GET_CLIENT> C_GET_CLIENT_Handler;
        private Action<Protocol.C_HEARTBEAT> C_HEARTBEAT_Handler;
        private Action<Protocol.C_PING> C_PING_Handler;
        private Action<Protocol.C_SERVERTIME> C_SERVERTIME_Handler;
        private Action<Protocol.C_TEST> C_TEST_Handler;
        private Action<Protocol.C_INSTANTIATE_GAME_OBJECT> C_INSTANTIATE_GAME_OBJECT_Handler;
        private Action<Protocol.C_GET_GAME_OBJECT> C_GET_GAME_OBJECT_Handler;
        private Action<Protocol.C_DESTORY_GAME_OBJECT> C_DESTORY_GAME_OBJECT_Handler;
        private Action<Protocol.C_SET_GAME_OBJECT_PREFAB> C_SET_GAME_OBJECT_PREFAB_Handler;
        private Action<Protocol.C_SET_GAME_OBJECT_OWNER> C_SET_GAME_OBJECT_OWNER_Handler;
        private Action<Protocol.C_SET_TRANSFORM> C_SET_TRANSFORM_Handler;
        private Action<Protocol.C_SET_ANIMATION> C_SET_ANIMATION_Handler;
        private Action<Protocol.C_FPS_POSITION> C_FPS_POSITION_Handler;
        private Action<Protocol.C_FPS_ROTATION> C_FPS_ROTATION_Handler;
        private Action<Protocol.C_FPS_SHOOT> C_FPS_SHOOT_Handler;
        private Action<Protocol.C_FPS_CHANGE_WEAPON> C_FPS_CHANGE_WEAPON_Handler;
        private Action<Protocol.C_FPS_RELOAD> C_FPS_RELOAD_Handler;
        private Action<Protocol.C_FPS_ANIMATION> C_FPS_ANIMATION_Handler;
        private Action<Protocol.C_FPS_READY> C_FPS_READY_Handler;
        private Action<Protocol.C_FPS_LOAD_COMPLETE> C_FPS_LOAD_COMPLETE_Handler;
        private Action<Protocol.C_FPS_START> C_FPS_START_Handler;
        private Action<Protocol.C_FPS_REPLAY> C_FPS_REPLAY_Handler;

        public PacketHandler()
        {
            Handlers.Add(0, _Handle_C_ENTER);
            Handlers.Add(2, _Handle_C_REENTER);
            Handlers.Add(4, _Handle_C_LEAVE);
            Handlers.Add(5, _Handle_C_GET_CLIENT);
            Handlers.Add(9, _Handle_C_HEARTBEAT);
            Handlers.Add(10, _Handle_C_PING);
            Handlers.Add(12, _Handle_C_SERVERTIME);
            Handlers.Add(14, _Handle_C_TEST);
            Handlers.Add(100, _Handle_C_INSTANTIATE_GAME_OBJECT);
            Handlers.Add(102, _Handle_C_GET_GAME_OBJECT);
            Handlers.Add(104, _Handle_C_DESTORY_GAME_OBJECT);
            Handlers.Add(107, _Handle_C_SET_GAME_OBJECT_PREFAB);
            Handlers.Add(109, _Handle_C_SET_GAME_OBJECT_OWNER);
            Handlers.Add(111, _Handle_C_SET_TRANSFORM);
            Handlers.Add(113, _Handle_C_SET_ANIMATION);
            Handlers.Add(201, _Handle_C_FPS_POSITION);
            Handlers.Add(203, _Handle_C_FPS_ROTATION);
            Handlers.Add(205, _Handle_C_FPS_SHOOT);
            Handlers.Add(208, _Handle_C_FPS_CHANGE_WEAPON);
            Handlers.Add(210, _Handle_C_FPS_RELOAD);
            Handlers.Add(212, _Handle_C_FPS_ANIMATION);
            Handlers.Add(214, _Handle_C_FPS_READY);
            Handlers.Add(216, _Handle_C_FPS_LOAD_COMPLETE);
            Handlers.Add(217, _Handle_C_FPS_START);
            Handlers.Add(301, _Handle_C_FPS_REPLAY);
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
        public void AddHandler( Action<Protocol.C_REENTER> handler )
        {
            C_REENTER_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_REENTER> handler )
        {
            C_REENTER_Handler -= handler;
        }
        private void _Handle_C_REENTER( IMessage message )
        {
            C_REENTER_Handler?.Invoke((Protocol.C_REENTER)message);
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
        public void AddHandler( Action<Protocol.C_HEARTBEAT> handler )
        {
            C_HEARTBEAT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_HEARTBEAT> handler )
        {
            C_HEARTBEAT_Handler -= handler;
        }
        private void _Handle_C_HEARTBEAT( IMessage message )
        {
            C_HEARTBEAT_Handler?.Invoke((Protocol.C_HEARTBEAT)message);
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
        public void AddHandler( Action<Protocol.C_INSTANTIATE_GAME_OBJECT> handler )
        {
            C_INSTANTIATE_GAME_OBJECT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_INSTANTIATE_GAME_OBJECT> handler )
        {
            C_INSTANTIATE_GAME_OBJECT_Handler -= handler;
        }
        private void _Handle_C_INSTANTIATE_GAME_OBJECT( IMessage message )
        {
            C_INSTANTIATE_GAME_OBJECT_Handler?.Invoke((Protocol.C_INSTANTIATE_GAME_OBJECT)message);
        }
        public void AddHandler( Action<Protocol.C_GET_GAME_OBJECT> handler )
        {
            C_GET_GAME_OBJECT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_GET_GAME_OBJECT> handler )
        {
            C_GET_GAME_OBJECT_Handler -= handler;
        }
        private void _Handle_C_GET_GAME_OBJECT( IMessage message )
        {
            C_GET_GAME_OBJECT_Handler?.Invoke((Protocol.C_GET_GAME_OBJECT)message);
        }
        public void AddHandler( Action<Protocol.C_DESTORY_GAME_OBJECT> handler )
        {
            C_DESTORY_GAME_OBJECT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_DESTORY_GAME_OBJECT> handler )
        {
            C_DESTORY_GAME_OBJECT_Handler -= handler;
        }
        private void _Handle_C_DESTORY_GAME_OBJECT( IMessage message )
        {
            C_DESTORY_GAME_OBJECT_Handler?.Invoke((Protocol.C_DESTORY_GAME_OBJECT)message);
        }
        public void AddHandler( Action<Protocol.C_SET_GAME_OBJECT_PREFAB> handler )
        {
            C_SET_GAME_OBJECT_PREFAB_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_SET_GAME_OBJECT_PREFAB> handler )
        {
            C_SET_GAME_OBJECT_PREFAB_Handler -= handler;
        }
        private void _Handle_C_SET_GAME_OBJECT_PREFAB( IMessage message )
        {
            C_SET_GAME_OBJECT_PREFAB_Handler?.Invoke((Protocol.C_SET_GAME_OBJECT_PREFAB)message);
        }
        public void AddHandler( Action<Protocol.C_SET_GAME_OBJECT_OWNER> handler )
        {
            C_SET_GAME_OBJECT_OWNER_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_SET_GAME_OBJECT_OWNER> handler )
        {
            C_SET_GAME_OBJECT_OWNER_Handler -= handler;
        }
        private void _Handle_C_SET_GAME_OBJECT_OWNER( IMessage message )
        {
            C_SET_GAME_OBJECT_OWNER_Handler?.Invoke((Protocol.C_SET_GAME_OBJECT_OWNER)message);
        }
        public void AddHandler( Action<Protocol.C_SET_TRANSFORM> handler )
        {
            C_SET_TRANSFORM_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_SET_TRANSFORM> handler )
        {
            C_SET_TRANSFORM_Handler -= handler;
        }
        private void _Handle_C_SET_TRANSFORM( IMessage message )
        {
            C_SET_TRANSFORM_Handler?.Invoke((Protocol.C_SET_TRANSFORM)message);
        }
        public void AddHandler( Action<Protocol.C_SET_ANIMATION> handler )
        {
            C_SET_ANIMATION_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_SET_ANIMATION> handler )
        {
            C_SET_ANIMATION_Handler -= handler;
        }
        private void _Handle_C_SET_ANIMATION( IMessage message )
        {
            C_SET_ANIMATION_Handler?.Invoke((Protocol.C_SET_ANIMATION)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_POSITION> handler )
        {
            C_FPS_POSITION_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_POSITION> handler )
        {
            C_FPS_POSITION_Handler -= handler;
        }
        private void _Handle_C_FPS_POSITION( IMessage message )
        {
            C_FPS_POSITION_Handler?.Invoke((Protocol.C_FPS_POSITION)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_ROTATION> handler )
        {
            C_FPS_ROTATION_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_ROTATION> handler )
        {
            C_FPS_ROTATION_Handler -= handler;
        }
        private void _Handle_C_FPS_ROTATION( IMessage message )
        {
            C_FPS_ROTATION_Handler?.Invoke((Protocol.C_FPS_ROTATION)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_SHOOT> handler )
        {
            C_FPS_SHOOT_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_SHOOT> handler )
        {
            C_FPS_SHOOT_Handler -= handler;
        }
        private void _Handle_C_FPS_SHOOT( IMessage message )
        {
            C_FPS_SHOOT_Handler?.Invoke((Protocol.C_FPS_SHOOT)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_CHANGE_WEAPON> handler )
        {
            C_FPS_CHANGE_WEAPON_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_CHANGE_WEAPON> handler )
        {
            C_FPS_CHANGE_WEAPON_Handler -= handler;
        }
        private void _Handle_C_FPS_CHANGE_WEAPON( IMessage message )
        {
            C_FPS_CHANGE_WEAPON_Handler?.Invoke((Protocol.C_FPS_CHANGE_WEAPON)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_RELOAD> handler )
        {
            C_FPS_RELOAD_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_RELOAD> handler )
        {
            C_FPS_RELOAD_Handler -= handler;
        }
        private void _Handle_C_FPS_RELOAD( IMessage message )
        {
            C_FPS_RELOAD_Handler?.Invoke((Protocol.C_FPS_RELOAD)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_ANIMATION> handler )
        {
            C_FPS_ANIMATION_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_ANIMATION> handler )
        {
            C_FPS_ANIMATION_Handler -= handler;
        }
        private void _Handle_C_FPS_ANIMATION( IMessage message )
        {
            C_FPS_ANIMATION_Handler?.Invoke((Protocol.C_FPS_ANIMATION)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_READY> handler )
        {
            C_FPS_READY_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_READY> handler )
        {
            C_FPS_READY_Handler -= handler;
        }
        private void _Handle_C_FPS_READY( IMessage message )
        {
            C_FPS_READY_Handler?.Invoke((Protocol.C_FPS_READY)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_LOAD_COMPLETE> handler )
        {
            C_FPS_LOAD_COMPLETE_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_LOAD_COMPLETE> handler )
        {
            C_FPS_LOAD_COMPLETE_Handler -= handler;
        }
        private void _Handle_C_FPS_LOAD_COMPLETE( IMessage message )
        {
            C_FPS_LOAD_COMPLETE_Handler?.Invoke((Protocol.C_FPS_LOAD_COMPLETE)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_START> handler )
        {
            C_FPS_START_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_START> handler )
        {
            C_FPS_START_Handler -= handler;
        }
        private void _Handle_C_FPS_START( IMessage message )
        {
            C_FPS_START_Handler?.Invoke((Protocol.C_FPS_START)message);
        }
        public void AddHandler( Action<Protocol.C_FPS_REPLAY> handler )
        {
            C_FPS_REPLAY_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.C_FPS_REPLAY> handler )
        {
            C_FPS_REPLAY_Handler -= handler;
        }
        private void _Handle_C_FPS_REPLAY( IMessage message )
        {
            C_FPS_REPLAY_Handler?.Invoke((Protocol.C_FPS_REPLAY)message);
        }
    }
}