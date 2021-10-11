using PacketLibrary.Logging;
using PacketLibrary.Network;
using System;

namespace Client
{
    class Client
    {
        public static readonly Logger Logger = Logger.LOGGER;

        public Guid Uuid;
        public string Name;

        public AClient SocketClient;
        public IConnection Connection;

        public Client(Guid uuid, string name)
        {
            Uuid = uuid;
            Name = name;
        }

        public void Connect(int port)
        {
            Connect("127.0.0.1", port);
        }

        public void Connect(string address, int port)
        {
            Logger.Info("Creating client with name {0} and unique identifier {1}", new object[] { Name, Uuid.ToString() });
            SocketClient = new ClientBootstrap(address, port);
            Connection = new DefaultConnection(new SimpleProtocol(), SocketClient.Connect());
        }
    }
}
