using Framework.Network;
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
        PKT_C_LEAVE = 2,
        PKT_C_GET_CLIENT = 3,
        PKT_S_ADD_CLIENT = 4,
        PKT_S_REMOVE_CLIENT = 5,
        PKT_S_DISCONNECT = 6,
        PKT_C_PING = 7,
        PKT_S_PING = 8,
        PKT_C_SERVERTIME = 9,
        PKT_S_SERVERTIME = 10,
        PKT_C_TEST = 11,
        PKT_S_TEST = 12,
        PKT_C_READY = 100,
        PKT_S_LOAD_SCENE = 101,
        PKT_C_LOAD_SCENE_COMPLETE = 102,
        PKT_S_START_GAME = 103,
        PKT_S_INSTANTIATE = 104,
        PKT_C_PLAYER_INPUT = 105,
        PKT_S_SPAWN_ITEM = 106,
        PKT_S_DESTROY_ITEM = 107,
        PKT_S_FINISH_GAME = 108,
    }

    public static class PacketManager
    {
        private static readonly Dictionary<ushort, Action<ArraySegment<byte>, ushort, Connection>> onRecv = new();

        static PacketManager()
        {
            onRecv.Add((ushort)MsgId.PKT_C_ENTER, MakePacket<C_ENTER>);
            onRecv.Add((ushort)MsgId.PKT_C_LEAVE, MakePacket<C_LEAVE>);
            onRecv.Add((ushort)MsgId.PKT_C_GET_CLIENT, MakePacket<C_GET_CLIENT>);
            onRecv.Add((ushort)MsgId.PKT_C_PING, MakePacket<C_PING>);
            onRecv.Add((ushort)MsgId.PKT_C_SERVERTIME, MakePacket<C_SERVERTIME>);
            onRecv.Add((ushort)MsgId.PKT_C_TEST, MakePacket<C_TEST>);
            onRecv.Add((ushort)MsgId.PKT_C_READY, MakePacket<C_READY>);
            onRecv.Add((ushort)MsgId.PKT_C_LOAD_SCENE_COMPLETE, MakePacket<C_LOAD_SCENE_COMPLETE>);
            onRecv.Add((ushort)MsgId.PKT_C_PLAYER_INPUT, MakePacket<C_PLAYER_INPUT>);
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
            
            if (id == (ushort)MsgId.PKT_C_PING)
            {
                Protocol.C_PING ping = pkt as Protocol.C_PING;
                connection.Handle_C_PING(ping);
            }
            
            connection.PacketQueue.Push(id, pkt);
        }
        
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_ENTER pkt ) { return MakeSendBuffer(pkt, 1); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_ADD_CLIENT pkt ) { return MakeSendBuffer(pkt, 4); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_REMOVE_CLIENT pkt ) { return MakeSendBuffer(pkt, 5); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_DISCONNECT pkt ) { return MakeSendBuffer(pkt, 6); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_PING pkt ) { return MakeSendBuffer(pkt, 8); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SERVERTIME pkt ) { return MakeSendBuffer(pkt, 10); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_TEST pkt ) { return MakeSendBuffer(pkt, 12); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_LOAD_SCENE pkt ) { return MakeSendBuffer(pkt, 101); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_START_GAME pkt ) { return MakeSendBuffer(pkt, 103); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_INSTANTIATE pkt ) { return MakeSendBuffer(pkt, 104); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_SPAWN_ITEM pkt ) { return MakeSendBuffer(pkt, 106); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_DESTROY_ITEM pkt ) { return MakeSendBuffer(pkt, 107); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.S_FINISH_GAME pkt ) { return MakeSendBuffer(pkt, 108); }

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