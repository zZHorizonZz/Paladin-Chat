using PacketLibrary.Network;

namespace Server.packet.server
{
    class PacketHandshake : Packet, ICodec
    {

        public string ServerName;
        public int OnlineClients;

        public PacketHandshake()
        {

        }

        public PacketHandshake(string serverName, int onlineClient)
        {
            ServerName = serverName;
            OnlineClients = onlineClient;
        }

        public Packet Decode(PacketBuffer buffer)
        {
            PacketHandshake packet = new PacketHandshake();
            packet.ServerName = buffer.ReadS;
            packet.OnlineClients = buffer.ReadInteger();
            return packet;
        }

        public PacketBuffer Encode(Packet packet, PacketBuffer buffer)
        {
            PacketHandshake handshake = (PacketHandshake) packet;
            buffer.WriteInteger;
            buffer.WriteInteger(handshake.OnlineClients);
            return buffer;
        }
    }
}
