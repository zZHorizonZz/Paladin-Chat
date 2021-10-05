using PacketLibrary.Network;
using System;

namespace Paladin
{
    class PongCodec : ICodec
    {
        public Packet Decode(PacketBuffer buffer)
        {
            long time = buffer.ReadLong();

            Console.WriteLine("Ping from from server has been accepted time {0} milliseconds.", time);
            return new PacketPong(time);
        }

        public PacketBuffer Encode(Packet packet, PacketBuffer buffer)
        {
            PacketPong pong = (PacketPong)packet;
            buffer.WriteLong(pong.Time);

            return buffer;
        }
    }
}
