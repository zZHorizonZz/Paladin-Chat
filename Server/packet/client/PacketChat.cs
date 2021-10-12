using PacketLibrary.Logging;
using PacketLibrary.Network;
using System;

namespace Server.Network
{
    class PacketChat : Packet, ICodec, IPacketHandler
    {
        public static readonly Logger Logger = Logger.LOGGER;

        public Guid Sender;
        public string SenderName;

        public string Message;

        public PacketChat()
        {

        }

        public PacketChat(Guid sender, string senderName, string message)
        {
            Sender = sender;
            SenderName = senderName;
            Message = message;
        }

        public Packet Decode(PacketBuffer buffer)
        {
            PacketChat packet = new PacketChat
            {
                Sender = Guid.Parse(buffer.ReadString()),
                SenderName = buffer.ReadString(),
                Message = buffer.ReadString()
            };

            return packet;
        }

        public PacketBuffer Encode(Packet packet, PacketBuffer buffer)
        {
            PacketChat chat = (PacketChat)packet;
            buffer.WriteString(chat.Sender.ToString());
            buffer.WriteString(chat.SenderName);
            buffer.WriteString(chat.Message);
            return buffer;
        }

        public void Handle(IConnection connection, Packet packet)
        {
            PacketChat chat = (PacketChat)packet;
            Logger.Info("[CHAT] {0}> {1}", new object[] { chat.SenderName, chat.Message });
        }
    }
}
