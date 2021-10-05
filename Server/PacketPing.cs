using PacketLibrary.Network;

namespace Server
{
    public class PacketPing : Packet
    {
        public long Time;

        public PacketPing()
        {

        }

        public PacketPing(long time)
        {
            Time = time;
        }
    }
}
