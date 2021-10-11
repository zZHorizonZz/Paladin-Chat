using PacketLibrary.Logging;
using PacketLibrary.Network;
using Server.Network;

namespace Server.client
{
    class ClientHandler
    {
        public static readonly Logger Logger = Logger.LOGGER;

        public void HandleConnection(PacketRequest request, IConnection connection)
        {
            PacketResponse response = new PacketResponse(ServerManager.Instance.Name, ServerManager.Instance.ClientContainer.Size(), (byte)(ServerManager.Instance.Public ? 0 : 1));
            connection.SendPacket(response);

            if (!ServerManager.Instance.Public)
            {
                connection.GetClient().Close();
                Logger.Info("Connection of client has been refused because server is not currently public.");
                return;
            }

            Client.Client client = ServerManager.Instance.ClientContainer.AddClient(request.ClientIdentifier, request.Name);
            client.Initialize(connection);

            Logger.Info("Client {0} has been succesfully connected to the server. ({1})", new object[] { client.Name, client.Uuid });
        }
    }
}
