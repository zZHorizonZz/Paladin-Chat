using PacketLibrary.Network;
using System;

namespace Paladin
{
    class PacketPingHandler : IPacketHandler
    {
        public void Handle(IConnection connection, Packet packet)
        {
            connection.SendPacket(new PacketPong(DateTimeOffset.Now.ToUnixTimeMilliseconds()));
        }
    }
}
