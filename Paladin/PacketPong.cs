using PacketLibrary.Network;

namespace Paladin
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
