using Google.Protobuf;
using Protocol;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public enum MsgId : ushort
    {
        PKT_C_ENTER = 0,
        PKT_S_ENTER = 1,
        PKT_C_REENTER = 2,
        PKT_S_REENTER = 3,
        PKT_C_LEAVE = 4,
        PKT_C_GET_CLIENT = 5,
        PKT_S_ADD_CLIENT = 6,
        PKT_S_REMOVE_CLIENT = 7,
        PKT_S_DISCONNECT = 8,
        PKT_C_HEARTBEAT = 9,
        PKT_C_PING = 10,
        PKT_S_PING = 11,
        PKT_C_SERVERTIME = 12,
        PKT_S_SERVERTIME = 13,
        PKT_C_TEST = 14,
        PKT_S_TEST = 15,
        PKT_C_INSTANTIATE_GAME_OBJECT = 100,
        PKT_S_INSTANTIATE_GAME_OBJECT = 101,
        PKT_C_GET_GAME_OBJECT = 102,
        PKT_S_ADD_GAME_OBJECT = 103,
        PKT_C_DESTORY_GAME_OBJECT = 104,
        PKT_S_DESTORY_GAME_OBJECT = 105,
        PKT_S_REMOVE_GAME_OBJECT = 106,
        PKT_C_SET_GAME_OBJECT_PREFAB = 107,
        PKT_S_SET_GAME_OBJECT_PREFAB = 108,
        PKT_C_SET_GAME_OBJECT_OWNER = 109,
        PKT_S_SET_GAME_OBJECT_OWNER = 110,
        PKT_C_SET_TRANSFORM = 111,
        PKT_S_SET_TRANSFORM = 112,
        PKT_C_SET_ANIMATION = 113,
        PKT_S_SET_ANIMATION = 114,
        PKT_S_FPS_INSTANTIATE = 200,
        PKT_C_FPS_POSITION = 201,
        PKT_S_FPS_POSITION = 202,
        PKT_C_FPS_ROTATION = 203,
        PKT_S_FPS_ROTATION = 204,
        PKT_C_FPS_SHOOT = 205,
        PKT_S_FPS_SHOOT = 206,
        PKT_S_FPS_ATTACKED = 207,
        PKT_C_FPS_CHANGE_WEAPON = 208,
        PKT_S_FPS_CHANGE_WEAPON = 209,
        PKT_C_FPS_RELOAD = 210,
        PKT_S_FPS_RELOAD = 211,
        PKT_C_FPS_ANIMATION = 212,
        PKT_S_FPS_ANIMATION = 213,
        PKT_C_FPS_READY = 214,
        PKT_S_FPS_LOAD = 215,
        PKT_C_FPS_LOAD_COMPLETE = 216,
        PKT_C_FPS_START = 217,
        PKT_S_FPS_START = 218,
        PKT_S_FPS_FINISH = 219,
        PKT_S_FPS_ANNOUNCE = 220,
        PKT_S_FPS_SPAWN_ITEM = 221,
        PKT_S_FPS_SPAWN_DESTINATION = 222,
        PKT_S_FPS_DESTROY_DESTINATION = 223,
        PKT_S_FPS_ITEM_OCCUPY_PROGRESS_STATE = 224,
        PKT_S_FPS_ITEM_OCCUPIED = 225,
        PKT_S_FPS_SCORED = 226,
        PKT_S_FPS_REPLAY = 300,
        PKT_C_FPS_REPLAY = 301,
    }

    public static class PacketManager
    {
        private static readonly Dictionary<ushort, Action<ArraySegment<byte>, ushort, Connection>> onRecv = new();

        static PacketManager()
        {
            onRecv.Add((ushort)MsgId.PKT_C_ENTER, MakePacket<C_ENTER>);
            onRecv.Add((ushort)MsgId.PKT_C_REENTER, MakePacket<C_REENTER>);
            onRecv.Add((ushort)MsgId.PKT_C_LEAVE, MakePacket<C_LEAVE>);
            onRecv.Add((ushort)MsgId.PKT_C_GET_CLIENT, MakePacket<C_GET_CLIENT>);
            onRecv.Add((ushort)MsgId.PKT_C_HEARTBEAT, MakePacket<C_HEARTBEAT>);
            onRecv.Add((ushort)MsgId.PKT_C_PING, MakePacket<C_PING>);
            onRecv.Add((ushort)MsgId.PKT_C_SERVERTIME, MakePacket<C_SERVERTIME>);
            onRecv.Add((ushort)MsgId.PKT_C_TEST, MakePacket<C_TEST>);
            onRecv.Add((ushort)MsgId.PKT_C_INSTANTIATE_GAME_OBJECT, MakePacket<C_INSTANTIATE_GAME_OBJECT>);
            onRecv.Add((ushort)MsgId.PKT_C_GET_GAME_OBJECT, MakePacket<C_GET_GAME_OBJECT>);
            onRecv.Add((ushort)MsgId.PKT_C_DESTORY_GAME_OBJECT, MakePacket<C_DESTORY_GAME_OBJECT>);
            onRecv.Add((ushort)MsgId.PKT_C_SET_GAME_OBJECT_PREFAB, MakePacket<C_SET_GAME_OBJECT_PREFAB>);
            onRecv.Add((ushort)MsgId.PKT_C_SET_GAME_OBJECT_OWNER, MakePacket<C_SET_GAME_OBJECT_OWNER>);
            onRecv.Add((ushort)MsgId.PKT_C_SET_TRANSFORM, MakePacket<C_SET_TRANSFORM>);
            onRecv.Add((ushort)MsgId.PKT_C_SET_ANIMATION, MakePacket<C_SET_ANIMATION>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_POSITION, MakePacket<C_FPS_POSITION>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_ROTATION, MakePacket<C_FPS_ROTATION>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_SHOOT, MakePacket<C_FPS_SHOOT>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_CHANGE_WEAPON, MakePacket<C_FPS_CHANGE_WEAPON>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_RELOAD, MakePacket<C_FPS_RELOAD>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_ANIMATION, MakePacket<C_FPS_ANIMATION>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_READY, MakePacket<C_FPS_READY>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_LOAD_COMPLETE, MakePacket<C_FPS_LOAD_COMPLETE>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_START, MakePacket<C_FPS_START>);
            onRecv.Add((ushort)MsgId.PKT_C_FPS_REPLAY, MakePacket<C_FPS_REPLAY>);
        }

        public static void OnRecv( ArraySegment<byte> buffer, Connection connection )
        {
            ushort count = 0;

            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            count += 2;
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
            count += 2;

            if (onRecv.TryGetValue(id, out Action<ArraySegment<byte>, ushort, Connection> action))
            {
                action.Invoke(buffer, id, connection);
            }
        }

        private static void MakePacket<T>( ArraySegment<byte> buffer, ushort id, Connection connection ) where T : IMessage, new()
        {
            T pkt = new();
            pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

            if (id == (ushort)MsgId.PKT_S_PING)
            {
                Protocol.S_PING ping = pkt as Protocol.S_PING;
                connection.Handle_S_PING(ping);
            }
            if (id == (ushort)MsgId.PKT_S_SERVERTIME)
            {
                Protocol.S_SERVERTIME serverTime = pkt as Protocol.S_SERVERTIME;
                connection.Handle_S_SERVERTIME(serverTime);
            }

            connection.PacketQueue.Push(id, pkt);
        }
        
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_ENTER pkt ) { return MakeSendBuffer(pkt, 1); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_REENTER pkt ) { return MakeSendBuffer(pkt, 3); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_ADD_CLIENT pkt ) { return MakeSendBuffer(pkt, 6); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_REMOVE_CLIENT pkt ) { return MakeSendBuffer(pkt, 7); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_DISCONNECT pkt ) { return MakeSendBuffer(pkt, 8); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_PING pkt ) { return MakeSendBuffer(pkt, 11); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SERVERTIME pkt ) { return MakeSendBuffer(pkt, 13); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_TEST pkt ) { return MakeSendBuffer(pkt, 15); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_INSTANTIATE_GAME_OBJECT pkt ) { return MakeSendBuffer(pkt, 101); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_ADD_GAME_OBJECT pkt ) { return MakeSendBuffer(pkt, 103); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_DESTORY_GAME_OBJECT pkt ) { return MakeSendBuffer(pkt, 105); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_REMOVE_GAME_OBJECT pkt ) { return MakeSendBuffer(pkt, 106); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SET_GAME_OBJECT_PREFAB pkt ) { return MakeSendBuffer(pkt, 108); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SET_GAME_OBJECT_OWNER pkt ) { return MakeSendBuffer(pkt, 110); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SET_TRANSFORM pkt ) { return MakeSendBuffer(pkt, 112); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SET_ANIMATION pkt ) { return MakeSendBuffer(pkt, 114); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_INSTANTIATE pkt ) { return MakeSendBuffer(pkt, 200); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_POSITION pkt ) { return MakeSendBuffer(pkt, 202); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_ROTATION pkt ) { return MakeSendBuffer(pkt, 204); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_SHOOT pkt ) { return MakeSendBuffer(pkt, 206); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_ATTACKED pkt ) { return MakeSendBuffer(pkt, 207); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_CHANGE_WEAPON pkt ) { return MakeSendBuffer(pkt, 209); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_RELOAD pkt ) { return MakeSendBuffer(pkt, 211); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_ANIMATION pkt ) { return MakeSendBuffer(pkt, 213); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_LOAD pkt ) { return MakeSendBuffer(pkt, 215); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_START pkt ) { return MakeSendBuffer(pkt, 218); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_FINISH pkt ) { return MakeSendBuffer(pkt, 219); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_ANNOUNCE pkt ) { return MakeSendBuffer(pkt, 220); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_SPAWN_ITEM pkt ) { return MakeSendBuffer(pkt, 221); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_SPAWN_DESTINATION pkt ) { return MakeSendBuffer(pkt, 222); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_DESTROY_DESTINATION pkt ) { return MakeSendBuffer(pkt, 223); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_ITEM_OCCUPY_PROGRESS_STATE pkt ) { return MakeSendBuffer(pkt, 224); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_ITEM_OCCUPIED pkt ) { return MakeSendBuffer(pkt, 225); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_SCORED pkt ) { return MakeSendBuffer(pkt, 226); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FPS_REPLAY pkt ) { return MakeSendBuffer(pkt, 300); }

        private static ArraySegment<byte> MakeSendBuffer( IMessage pkt, ushort pktId )
        {
            ushort size = (ushort)pkt.CalculateSize();
            byte[] sendBuffer = new byte[size + 4];

            Array.Copy(BitConverter.GetBytes((ushort)(size + 4)), 0, sendBuffer, 0, sizeof(ushort));
            Array.Copy(BitConverter.GetBytes(pktId), 0, sendBuffer, 2, sizeof(ushort));
            Array.Copy(pkt.ToByteArray(), 0, sendBuffer, 4, size);

            return sendBuffer;
        }
    }
}