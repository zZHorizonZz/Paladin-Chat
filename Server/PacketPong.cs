using PacketLibrary.Network;

namespace Server
{
    public class PacketPong : Packet
    {
        public long Time;

        public PacketPong()
        {

        }

        public PacketPong(long time)
        {
            Time = time;
        }
    }
}