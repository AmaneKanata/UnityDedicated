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
    }

    public static class PacketManager
    {
        private static readonly Dictionary<ushort, Action<ArraySegment<byte>, ushort, Connection>> onRecv = new();

        static PacketManager()
        {
            onRecv.Add((ushort)MsgId.PKT_S_ENTER, MakePacket<S_ENTER>);
            onRecv.Add((ushort)MsgId.PKT_S_ADD_CLIENT, MakePacket<S_ADD_CLIENT>);
            onRecv.Add((ushort)MsgId.PKT_S_REMOVE_CLIENT, MakePacket<S_REMOVE_CLIENT>);
            onRecv.Add((ushort)MsgId.PKT_S_DISCONNECT, MakePacket<S_DISCONNECT>);
            onRecv.Add((ushort)MsgId.PKT_S_PING, MakePacket<S_PING>);
            onRecv.Add((ushort)MsgId.PKT_S_SERVERTIME, MakePacket<S_SERVERTIME>);
            onRecv.Add((ushort)MsgId.PKT_S_TEST, MakePacket<S_TEST>);
            onRecv.Add((ushort)MsgId.PKT_S_LOAD_SCENE, MakePacket<S_LOAD_SCENE>);
            onRecv.Add((ushort)MsgId.PKT_S_START_GAME, MakePacket<S_START_GAME>);
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
        
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_ENTER pkt ) { return MakeSendBuffer(pkt, 0); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_LEAVE pkt ) { return MakeSendBuffer(pkt, 2); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_GET_CLIENT pkt ) { return MakeSendBuffer(pkt, 3); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_PING pkt ) { return MakeSendBuffer(pkt, 7); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_SERVERTIME pkt ) { return MakeSendBuffer(pkt, 9); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_TEST pkt ) { return MakeSendBuffer(pkt, 11); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_READY pkt ) { return MakeSendBuffer(pkt, 100); }
        public static ArraySegment<byte> MakeSendBuffer( Protocol.C_LOAD_SCENE_COMPLETE pkt ) { return MakeSendBuffer(pkt, 102); }

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