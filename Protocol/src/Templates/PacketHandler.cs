using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Framework.Network
{
    public class PacketHandler
    {
        public Dictionary<ushort, Action<IMessage>> Handlers = new();

        {%- for pkt in parser.recv_pkt %}
        private Action<Protocol.{{pkt.name}}> {{pkt.name}}_Handler;
        {%- endfor %}

        public PacketHandler()
        {
            {%- for pkt in parser.recv_pkt %}
            Handlers.Add({{pkt.id}}, _Handle_{{pkt.name}});
            {%- endfor %}
        }

        {%- for pkt in parser.recv_pkt %}
        public void AddHandler( Action<Protocol.{{pkt.name}}> handler )
        {
            {{pkt.name}}_Handler += handler;
        }
        public void RemoveHandler( Action<Protocol.{{pkt.name}}> handler )
        {
            {{pkt.name}}_Handler -= handler;
        }
        private void _Handle_{{pkt.name}}( IMessage message )
        {
            {{pkt.name}}_Handler?.Invoke((Protocol.{{pkt.name}})message);
        }
        {%- endfor %}
    }
}