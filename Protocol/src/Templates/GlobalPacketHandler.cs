using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class GlobalPacketHandler
    {
        public Dictionary<ushort, Action<IMessage, Connection>> Handlers = new();

        {%- for pkt in parser.recv_pkt %}
        private Action<Protocol.{{pkt.name}}, Connection> {{pkt.name}}_Handler;
        {%- endfor %}

        public GlobalPacketHandler()
        {
            {%- for pkt in parser.recv_pkt %}
            Handlers.Add({{pkt.id}}, _Handle_{{pkt.name}});
            {%- endfor %}
        }

        {%- for pkt in parser.recv_pkt %}
        public void AddHandler( Action<Protocol.{{pkt.name}}, Connection> handler )
        {
            {{pkt.name}}_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.{{pkt.name}}, Connection> handler )
        {
            {{pkt.name}}_Handler -= handler;
        }
        private void _Handle_{{pkt.name}}( IMessage message, Connection connection )
        {
            {{pkt.name}}_Handler?.Invoke((Protocol.{{pkt.name}})message, connection);
        }
        {%- endfor %}
    }
}