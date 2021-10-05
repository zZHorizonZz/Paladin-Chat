using PacketLibrary.Network;
using System;

namespace Paladin
{
    class PingCodec : ICodec
    {
        public Packet Decode(PacketBuffer buffer)
        {
            long time = buffer.ReadLong();

            Console.WriteLine("Ping from from server has been accepted time {0} milliseconds.", time);
            return new PacketPing(time);
        }

        public PacketBuffer Encode(Packet packet, PacketBuffer buffer)
        {
            PacketPing ping = (PacketPing)packet;
            buffer.WriteLong(ping.Time);

            return buffer;
        }
    }
}
