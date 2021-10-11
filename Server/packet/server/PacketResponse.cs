using PacketLibrary.Logging;
using PacketLibrary.Network;

namespace Server.Network
{
    public class PacketResponse : Packet, ICodec
    {
        static readonly Logger Logger = Logger.LOGGER;

        public string ServerName;
        public int OnlineClients;
        public byte ResponseCode = 0;

        public PacketResponse()
        {

        }

        public PacketResponse(string serverName, int onlineClient, byte responseCode)
        {
            ServerName = serverName;
            OnlineClients = onlineClient;
            ResponseCode = responseCode;
        }

        public Packet Decode(PacketBuffer buffer)
        {
            PacketResponse packet = new PacketResponse();
            packet.ServerName = buffer.ReadString();
            packet.OnlineClients = buffer.ReadInteger();
            packet.ResponseCode = buffer.ReadByte();
            return packet;
        }

        public PacketBuffer Encode(Packet packet, PacketBuffer buffer)
        {
            PacketResponse response = (PacketResponse)packet;
            buffer.WriteString(response.ServerName);
            buffer.WriteInteger(response.OnlineClients);
            buffer.WriteByte(response.ResponseCode);
            return buffer;
        }

        public void Handle(IConnection connection, Packet packet)
        {
            PacketResponse response = (PacketResponse)packet;
            if (response.ResponseCode == 1)
            {
                connection.GetClient().Close();
                Logger.Info("Connection has been refused by the server.");
                return;
            }

            Logger.Info("Connected to server {0} with {1} client's online.", new object[] { response.ServerName, response.OnlineClients });
        }
    }
}
