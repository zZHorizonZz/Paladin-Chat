using PacketLibrary.Logging;
using PacketLibrary.Network;
using System;

namespace Client.Network
{
    public class PacketRequest : Packet, ICodec, IPacketHandler
    {
        static readonly Logger Logger = Logger.LOGGER;

        public string Name;
        public Guid ClientIdentifier;

        public PacketRequest()
        {

        }

        public PacketRequest(string name, Guid clientIdentifier)
        {
            Name = name;
            ClientIdentifier = clientIdentifier;
        }

        public Packet Decode(PacketBuffer buffer)
        {
            PacketRequest packet = new PacketRequest
            {
                Name = buffer.ReadString(),
                ClientIdentifier = Guid.Parse(buffer.ReadString())
            };

            return packet;
        }

        public PacketBuffer Encode(Packet packet, PacketBuffer buffer)
        {
            PacketRequest response = (PacketRequest)packet;
            buffer.WriteString(response.Name);
            buffer.WriteString(response.ClientIdentifier.ToString());
            return buffer;
        }

        public void Handle(IConnection connection, Packet packet)
        {

        }
    }
}


