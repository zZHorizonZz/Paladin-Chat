using PacketLibrary.Network;

namespace Paladin
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
