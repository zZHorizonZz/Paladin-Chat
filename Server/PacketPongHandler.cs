using PacketLibrary.Logging;
using PacketLibrary.Network;

namespace Server
{
    class PacketPongHandler : IPacketHandler
    {
        public Logger Logger = Logger.LOGGER;

        public void Handle(IConnection connection, Packet packet)
        {
            PacketPong pong = (PacketPong)packet;
            Logger.Debug("Response time of ping is {0} milliseconds.", new object[] { pong.Time });
        }
    }
}

